﻿using CQRS;
using Doc.Management.Documents;
using Doc.Management.Documents.Commands;
using Doc.Management.Documents.DataModels;
using Doc.Management.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Doc.Management.Api.Documents;

internal static class Endpoints
{
    public static WebApplication MapDocuments(this WebApplication app)
    {
        app.MapPost("api/documents", CreateDocument)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .DisableAntiforgery()
            .WithName("CreateDocument")
            .WithOpenApi();

        app.MapPut("api/documents/{id}", UpdateDocument)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .DisableAntiforgery()
            .WithName("UpdateDocument")
            .WithOpenApi();

        app.MapGet("api/documents/{id}", GetDocument)
            .WithName("GetDocument")
            .Produces<FileStreamHttpResult>()
            .WithOpenApi();

        app.MapDelete("api/documents/{id}", DeleteDocument)
            .WithName("DeleteDocument")
            .WithOpenApi();

        return app;
    }

    private static async Task<Results<FileStreamHttpResult, NotFound>> GetDocument(
        [FromServices] IReadDocuments documentReader,
        [FromServices] IStoreFile fileStore,
        [FromRoute] Guid id,
        [FromQuery] string? version,
        CancellationToken cancellationToken = default
    )
    {
        var document = await documentReader.GetDocumentByIdAsync(
            id,
            version != null ? Version.Parse(version) : null,
            cancellationToken
        );

        if (document == null)
        {
            return TypedResults.NotFound();
        }

        var fileStream = await fileStore.GetStreamAsync(document.Key, cancellationToken);

        return TypedResults.File(
            fileStream,
            fileDownloadName: $"{document.Name}.{document.Extension}"
        );
    }

    private static async Task<Ok> DeleteDocument(
        [FromServices] IMediator mediator,
        [FromServices] IContext context,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var command = new WrappedCommand<DeleteDocument, Document>(
            new DeleteDocument(id),
            context.UserId
        );
        await mediator.Send(command, cancellationToken);

        return TypedResults.Ok();
    }

    public static async Task<
        Results<Created<DocumentDocument[]>, BadRequest<string>>
    > CreateDocument(
        [FromServices] IMediator mediator,
        [FromServices] IStoreFile fileStore,
        [FromServices] IContext context,
        IFormFileCollection files,
        CancellationToken cancellationToken = default
    )
    {
        var results = new List<DocumentDocument>();

        //TODO: revoir cette méthode pour optimiser le multi upload
        foreach (var file in files)
        {
            var fileInfos = await StoreFile(file, fileStore, cancellationToken);

            var command = new WrappedCommand<CreateDocument, Document>(
                new CreateDocument(
                    fileInfos.Key,
                    file.FileName,
                    fileInfos.FileNameWithoutExtension,
                    fileInfos.Extension
                ),
                context.UserId
            );

            var result = await mediator.Send(command, cancellationToken);

            var document = new DocumentDocument(
                result.Id,
                result.Key,
                result.Name,
                result.FileNameWIthoutExtension,
                result.Extension,
                result.DocumentVersion
            );

            results.Add(document);
        }

        return TypedResults.Created("", results.ToArray());
    }

    public static async Task<Results<Ok, BadRequest<string>>> UpdateDocument(
        [FromServices] IMediator mediator,
        [FromServices] IStoreFile fileStore,
        [FromServices] IContext context,
        [FromRoute] Guid id,
        IFormFile files,
        VersionIncrementType versionIncrementType,
        CancellationToken cancellationToken = default
    )
    {
        var fileInfos = await StoreFile(files, fileStore, cancellationToken);

        var command = new WrappedCommand<ModifyDocument, Document>(
            new ModifyDocument(
                id,
                fileInfos.Key,
                files.FileName,
                fileInfos.FileNameWithoutExtension,
                fileInfos.Extension,
                versionIncrementType
            ),
            context.UserId
        );

        await mediator.Send(command, cancellationToken);

        return TypedResults.Ok();
    }

    private static async Task<FileInfos> StoreFile(
        IFormFile uploadFile,
        IStoreFile fileStore,
        CancellationToken cancellationToken = default
    )
    {
        if (uploadFile == null || uploadFile.Length == 0)
        {
            throw new ArgumentException("No file selected");
        }

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(uploadFile.FileName);
        var extension = Path.GetExtension(uploadFile.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(extension))
        {
            throw new ArgumentException("Invalid file type");
        }

        var key = DocumentKey.NewDocumentKey();

        using (var stream = new MemoryStream())
        {
            await uploadFile.CopyToAsync(stream, cancellationToken);

            await fileStore.UploadStreamAsync(stream, key, cancellationToken).ConfigureAwait(false);
        }

        return new(key, fileNameWithoutExtension, extension);
    }
}

internal record struct FileInfos(
    DocumentKey Key,
    string FileNameWithoutExtension,
    string Extension
)
{ }
