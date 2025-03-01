using CQRS;

namespace Journalist.Crm.CommandHandlers;

public static class ErrorBuilder
{
    public static Error AggregateNotFound() => new(Errors.AggregateNotFound.CODE, Errors.AggregateNotFound.MESSAGE);
}
