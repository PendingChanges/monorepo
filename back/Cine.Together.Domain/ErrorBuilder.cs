using CQRS;

namespace Cine.Together.Domain;

public static class ErrorBuilder
{
    public static Error AggregateNotFound() => new(Errors.AggregateNotFound.CODE, Errors.AggregateNotFound.MESSAGE);
}
