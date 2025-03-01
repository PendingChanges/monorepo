using CQRS;
using Journalist.Crm.Domain.Clients.Events;

namespace Journalist.Crm.Domain.Clients;

public sealed class Client : Aggregate
{
    public string Name { get; private set; }
    public string OwnerId { get; private set; }
    public bool Deleted { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Client() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public AggregateResult Create(string name, string ownerId)
    {
        var result = AggregateResult.Create();

        var id = Guid.NewGuid();

        var @event = new ClientCreated(id, name, ownerId);

        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    public AggregateResult Delete(string ownerId)
    {
        var result = AggregateResult.Create();

        result.CheckAndAddError(() => OwnerId != ownerId, WellKnownErrors.NotClientOwner());

        if (!result.HasErrors)
        {
            var @event = new ClientDeleted(Id);
            Apply(@event);
            result.AddEvent(@event);
        }

        return result;
    }

    public AggregateResult Rename(string newName, string ownerId)
    {
        var result = AggregateResult.Create();
        result.CheckAndAddError(() => OwnerId != ownerId, WellKnownErrors.NotClientOwner());

        if (result.HasErrors)
        {
            return result;
        }

        if (string.CompareOrdinal(Name, newName) == 0)
        {
            return result;
        }

        var @event = new ClientRenamed(Id, newName);
        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    private void Apply(ClientRenamed @event)
    {
        Name = @event.NewName;
        IncrementVersion();
    }

    private void Apply(ClientCreated @event)
    {
        SetId(@event.Id);

        Name = @event.Name;
        OwnerId = @event.OwnerId;
        Deleted = false;

        IncrementVersion();
    }

#pragma warning disable S1172 // Unused method parameters should be removed
    private void Apply(ClientDeleted @event)
#pragma warning restore S1172 // Unused method parameters should be removed
    {
        Deleted = true;
        IncrementVersion();
    }
}
