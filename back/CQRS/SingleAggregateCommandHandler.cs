using MediatR;

namespace CQRS;

public abstract class SingleAggregateCommandHandler<TCommand, TAggregate>(IWriteEvents eventWriter, IReadAggregates aggregateReader) : IRequestHandler<WrappedCommand<TCommand, TAggregate>, TAggregate>
    where TAggregate : Aggregate
    where TCommand : ICommand
{
    protected readonly IWriteEvents EventWriter = eventWriter;
    protected readonly IReadAggregates AggregateReader = aggregateReader;

    public async Task<TAggregate> Handle(WrappedCommand<TCommand, TAggregate> request, CancellationToken cancellationToken)
    {
        var command = request.Command;

        var aggregate = await LoadAggregate(command, request.UserId, cancellationToken) ?? throw new DomainException([new DomainError("AGGREGATE_NOT_FOUND", "Aggregate not found")]);
        var aggregateResult = ExecuteCommand(aggregate, command, request.UserId);

        if (aggregateResult.HasErrors)
        {
            throw new DomainException(aggregateResult.GetErrors());
        }

        await EventWriter.StoreAsync(aggregate.Id, aggregate.Version, aggregateResult.GetEvents(), cancellationToken);

        return aggregate;
    }

    protected abstract Task<TAggregate?> LoadAggregate(TCommand command, string ownerId, CancellationToken cancellationToken);

    protected abstract AggregateResult ExecuteCommand(TAggregate aggregate, TCommand command, string ownerId);

}
