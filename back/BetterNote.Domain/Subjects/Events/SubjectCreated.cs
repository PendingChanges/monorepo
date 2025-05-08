namespace BetterNote.Domain.Subjects.Events;

public sealed record SubjectCreated(Guid SubjectId, string Title, string Description);
