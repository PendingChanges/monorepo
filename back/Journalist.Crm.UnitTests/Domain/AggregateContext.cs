using CQRS;

namespace Journalist.Crm.UnitTests.Domain;

public class AggregateContext
{
    public Aggregate? Aggregate { get; set; }

    public AggregateResult? Result { get; set; }

    public List<object> GetEvents() => Result?.GetEvents().ToList() ?? [];

    public List<DomainError> GetErrors() => Result?.GetErrors().ToList() ?? [];
}
