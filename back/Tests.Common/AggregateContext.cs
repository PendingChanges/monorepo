using CQRS;

namespace Tests.Common;

public sealed class AggregateContext
{
    public Aggregate? Aggregate { get; set; }

    public AggregateResult? Result { get; set; }

    public List<object> GetEvents() => Result?.GetEvents().ToList() ?? [];

    public List<DomainError> GetErrors() => Result?.GetErrors().ToList() ?? [];
}
