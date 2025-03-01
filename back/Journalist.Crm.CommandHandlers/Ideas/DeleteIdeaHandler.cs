using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using CQRS;

namespace Journalist.Crm.CommandHandlers.Ideas;

internal class DeleteIdeaHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<DeleteIdea, Idea>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Idea aggregate, DeleteIdea command, string ownerId) => aggregate.Delete(ownerId);

    protected override Task<Idea?> LoadAggregate(DeleteIdea command, string ownerId, CancellationToken cancellationToken) => AggregateReader.LoadAsync<Idea>(command.Id, cancellationToken: cancellationToken);
}
