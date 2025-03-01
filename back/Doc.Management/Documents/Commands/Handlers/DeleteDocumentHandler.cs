using CQRS;

namespace Doc.Management.Documents.Commands.Handlers;

internal class DeleteDocumentHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<DeleteDocument, Document>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(
        Document aggregate,
        DeleteDocument command,
        string ownerId
    ) => aggregate.Delete(ownerId);

    protected override Task<Document?> LoadAggregate(
        DeleteDocument command,
        string ownerId,
        CancellationToken cancellationToken
    ) => AggregateReader.LoadAsync<Document>(command.Id, cancellationToken: cancellationToken);
}
