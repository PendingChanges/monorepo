using CQRS;

namespace Journalist.Crm.CommandHandlers;

public static class ErrorBuilder
{
    public static DomainError AggregateNotFound() => new(Errors.AggregateNotFound.CODE, Errors.AggregateNotFound.MESSAGE);
}
