using BetterNote.Application.Subjects;
using BetterNote.Application.Tags;
using BetterNote.Domain.Subjects.Events;
using Marten;
using Marten.Events.Projections;

namespace BetterNote.Infrastructure.Marten.Subjects;
public class SubjectProjection : EventProjection
{
    public static SubjectDocument Create(SubjectCreated subjectCreated)
        => new(subjectCreated.Id, subjectCreated.Title, subjectCreated.Description);

    public static void Project(SubjectTagged @event, IDocumentOperations ops) => ops.Store(new TaggingDocument(@event.Id, @event.TagId));
}
