using CQRS;

namespace Doc.Management.Documents.Commands.Handlers;

internal class ModifyDocumentHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<ModifyDocument, Document>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(
        Document aggregate,
        ModifyDocument command,
        string ownerId
    ) =>
        aggregate.Modify(
            command.Key,
            command.Name,
            command.FileNameWithoutExtension,
            command.Extension,
            command.VersionIncrementType,
            ownerId
        );

    protected override Task<Document?> LoadAggregate(
        ModifyDocument command,
        string ownerId,
        CancellationToken cancellationToken
    ) =>
        AggregateReader.LoadAsync<Document>(
            command.DocumentId,
            cancellationToken: cancellationToken
        );
}
