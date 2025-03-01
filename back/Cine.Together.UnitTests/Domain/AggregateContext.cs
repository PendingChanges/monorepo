using CQRS;

namespace Cine.Together.UnitTests.Domain;

public class AggregateContext
{
    public Aggregate? Aggregate { get; set; }

    public AggregateResult? Result { get; set; }

    public List<object> GetEvents() => Result?.GetEvents().ToList() ?? [];

    public List<Error> GetErrors() => Result?.GetErrors().ToList() ?? [];
}
