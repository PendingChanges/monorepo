namespace CQRS;

public class EventCollection
{
    private readonly List<object> _events = [];

    public void Add(object @event) => _events.Add(@event);

    public IEnumerable<object> GetEvents() => _events;
}
