namespace Journalist.Crm.Domain.Pitches.Events;

public sealed record PitchCreated(Guid Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, Guid ClientId, Guid IdeaId, string OwnerId);
