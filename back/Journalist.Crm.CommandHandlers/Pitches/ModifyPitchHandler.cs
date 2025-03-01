using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using CQRS;

namespace Journalist.Crm.CommandHandlers.Pitches;

internal class ModifyPitchHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<ModifyPitch, Pitch>(eventWriter, aggregateReader)
{
    protected override Task<Pitch?> LoadAggregate(ModifyPitch command, string ownerId, CancellationToken cancellationToken)
        => AggregateReader.LoadAsync<Pitch>(command.Id, cancellationToken: cancellationToken);

    protected override AggregateResult ExecuteCommand(Pitch aggregate, ModifyPitch command, string ownerId)
        => aggregate.Modify(command.Content, command.DeadLineDate, command.IssueDate, command.ClientId, command.IdeaId, ownerId);
}
