namespace BetterNote.Domain.Subjects.Events;

public record SubjectCreated(Guid SubjectId, string Title, string Description);
