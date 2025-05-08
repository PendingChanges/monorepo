namespace BetterNote.Domain.Subjects.Events;
public sealed record TagAddedToSubject(Guid SubjectId, Guid TagId);
