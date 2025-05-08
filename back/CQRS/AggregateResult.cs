namespace CQRS;

public sealed class AggregateResult
{
    private AggregateResult()
    {

    }
    private readonly DomainErrorCollection _errors = new();
    private readonly EventCollection _events = new();
    public bool HasErrors => _errors.HasErrors;

    public void AddEvent(object @event) => _events.Add(@event);
    public void AddError(DomainError error) => _errors.AddError(error);

    public void CheckAndAddError(Func<bool> check, DomainError error)
    {
        if (check())
        {
            AddError(error);
        }
    }

    public IEnumerable<DomainError> GetErrors() => _errors.GetErrors();

    public static AggregateResult Create() => new();

    public IEnumerable<object> GetEvents() => _events.GetEvents();
}