using CQRS;

namespace Cine.Together.Domain;

public static class ErrorBuilder
{
    public static DomainError AggregateNotFound() => new(Errors.AggregateNotFound.CODE, Errors.AggregateNotFound.MESSAGE);
}
