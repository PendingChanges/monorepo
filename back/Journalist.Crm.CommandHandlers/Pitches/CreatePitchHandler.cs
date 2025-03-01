using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using CQRS;

namespace Journalist.Crm.CommandHandlers.Pitches;

internal class CreatePitchHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<CreatePitch, Pitch>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Pitch aggregate, CreatePitch command, string ownerId) => aggregate.Create(command.Content, command.DeadLineDate, command.IssueDate, command.ClientId, command.IdeaId, ownerId);

    protected override Task<Pitch?> LoadAggregate(CreatePitch command, string ownerId,
        CancellationToken cancellationToken)
    {
        var pitch = new Pitch();
        return Task.FromResult<Pitch?>(pitch);
    }
}
