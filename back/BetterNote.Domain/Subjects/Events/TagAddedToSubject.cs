namespace BetterNote.Domain.Subjects.Events;
public record TagAddedToSubject(Guid SubjectId, Guid TagId);
