using BetterNote.Domain.Persons.Events;
using BetterNote.Domain.Subjects.Events;
using CQRS;

namespace BetterNote.Domain.Persons;
internal class Person : Aggregate
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Person()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public AggregateResult CreatePerson(string FirstName, string LastName)
    {
        var result = AggregateResult.Create();
        var id = Guid.NewGuid();
        var @event = new PersonCreated(id, FirstName, LastName);

        Apply(@event);
        result.AddEvent(@event);
        return result;
    }

    private void Apply(PersonCreated @event)
    {
        SetId(@event.PersonId);

        FirstName = @event.FirstName;
        LastName = @event.LastName;

        IncrementVersion();
    }
}
