namespace BetterNote.Domain.Subjects.Events;

public sealed record SubjectCreated(Guid Id, string Title, string Description);
