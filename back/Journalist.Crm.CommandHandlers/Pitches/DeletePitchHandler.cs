using CQRS;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;

namespace Journalist.Crm.CommandHandlers.Pitches;

internal class DeletePitchHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<DeletePitch, Pitch>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Pitch aggregate, DeletePitch command, string ownerId)
        => aggregate.Cancel(ownerId);

    protected override Task<Pitch?> LoadAggregate(DeletePitch command, string ownerId, CancellationToken cancellationToken)
        => AggregateReader.LoadAsync<Pitch>(command.Id, cancellationToken: cancellationToken);
}
