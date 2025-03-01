using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using CQRS;

namespace Journalist.Crm.CommandHandlers.Ideas;

internal class ModifyIdeaHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<ModifyIdea, Idea>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Idea aggregate, ModifyIdea command, string ownerId) => aggregate.Modify(command.NewName, command.NewDescription, ownerId);

    protected override Task<Idea?> LoadAggregate(ModifyIdea command, string ownerId, CancellationToken cancellationToken) => AggregateReader.LoadAsync<Idea>(command.Id, cancellationToken: cancellationToken);
}
