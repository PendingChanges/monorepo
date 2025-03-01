using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using CQRS;

namespace Journalist.Crm.CommandHandlers.Ideas;

internal class CreateIdeaHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<CreateIdea, Idea>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Idea aggregate, CreateIdea command, string ownerId) => aggregate.Create(command.Name, command.Description, ownerId);

    protected override Task<Idea?> LoadAggregate(CreateIdea command, string ownerId,
        CancellationToken cancellationToken)
    {
        var idea = new Idea();
        return Task.FromResult<Idea?>(idea);
    }
}
