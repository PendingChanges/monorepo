﻿using BetterNote.Domain.Tags.Events;
using CQRS;

namespace BetterNote.Domain.Tags;
public sealed class Tag : Aggregate
{
    public string Value { get; set; }
    public bool Deleted { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Tag() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AggregateResult CreateTag(string value)
    {
        var result = AggregateResult.Create();

        var id = Guid.NewGuid();

        var @event = new TagCreated(id, value);

        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    public AggregateResult Delete()
    {
        var result = AggregateResult.Create();

        if (Deleted)
        {
            //TODO : create an error builder
            result.AddError(new Error(WellKnownErrors.TagAlreadyDeleted, WellKnownErrors.Messages.GetValueOrDefault(WellKnownErrors.TagAlreadyDeleted) ?? "Unknwon"));
            return result;
        }

        var @event = new TagDeleted(Id);
        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    private void Apply(TagCreated @event)
    {
        SetId(@event.Id);

        Value = @event.Value;

        IncrementVersion();
    }

#pragma warning disable S1172 // Unused method parameters should be removed
    private void Apply(TagDeleted _)
#pragma warning restore S1172 // Unused method parameters should be removed
    {
        Deleted = true;

        IncrementVersion();
    }
}
