﻿using BetterNote.Domain.Subjects.Events;
using CQRS;

namespace BetterNote.Domain.Subjects;
public sealed class Subject: Aggregate
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Subject()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyCollection<Guid> TagsId { get; private set; }

    public AggregateResult CreateSubject(string Title, string Description, IReadOnlyCollection<Guid> tagsId)
    {
        var result = AggregateResult.Create();
        var id = Guid.NewGuid();
        var @event = new SubjectCreated(id, Title, Description, tagsId);
       
        Apply(@event);
        result.AddEvent(@event);
        return result;
    }

    private void Apply(SubjectCreated @event)
    {
        SetId(@event.Id);

        Title = @event.Title;
        Description = @event.Description;
        TagsId = @event.TagsId;

        IncrementVersion();
    }
}
