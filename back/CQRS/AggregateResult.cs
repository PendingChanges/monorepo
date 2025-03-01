namespace CQRS;

public class AggregateResult
{
    private AggregateResult()
    {

    }
    private readonly ErrorCollection _errors = new();
    private readonly EventCollection _events = new();
    public bool HasErrors => _errors.HasErrors;

    public void AddEvent(object @event) => _events.Add(@event);
    public void AddError(Error error) => _errors.AddError(error);

    public void CheckAndAddError(Func<bool> check, Error error)
    {
        if (check())
        {
            AddError(error);
        }
    }

    public IEnumerable<Error> GetErrors() => _errors.GetErrors();

    public static AggregateResult Create() => new();

    public IEnumerable<object> GetEvents() => _events.GetEvents();
}