using BetterNote.Application.Subjects;
using BetterNote.Domain.Subjects.Events;
using Marten.Events.Projections;

namespace BetterNote.Infrastructure.Marten.Subjects;
public class SubjectProjection : EventProjection
{
    public SubjectDocument Create(SubjectCreated subjectCreated)
        => new(subjectCreated.Id, subjectCreated.Title, subjectCreated.Description);
}
