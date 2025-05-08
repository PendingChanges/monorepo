using MediatR;

namespace CQRS;

public sealed class WrappedCommand<TCommand, TAggregate>(TCommand command, string userId) : IRequest<TAggregate>
    where TCommand : ICommand
    where TAggregate : Aggregate
{
    public TCommand Command { get; } = command;

    public string UserId { get; } = userId;
}
