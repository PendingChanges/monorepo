namespace Journalist.Crm.Domain.Pitches.Events;

public sealed record PitchIssueRescheduled(Guid Id, DateTime? IssueDate);
