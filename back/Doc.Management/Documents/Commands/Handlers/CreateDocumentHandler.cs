using CQRS;

namespace Doc.Management.Documents.Commands.Handlers;

internal class CreateDocumentHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<CreateDocument, Document>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(
        Document aggregate,
        CreateDocument command,
        string ownerId
    ) =>
        aggregate.CreateDocument(
            command.Key,
            command.Name,
            command.FileNameWithoutExtension,
            command.Extension,
            ownerId
        );

    protected override Task<Document?> LoadAggregate(
        CreateDocument command,
        string ownerId,
        CancellationToken cancellationToken
    ) => Task.FromResult<Document?>(new Document());
}
