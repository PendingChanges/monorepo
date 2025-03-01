namespace Journalist.Crm.Domain.Pitches.Events;

public record PitchIssueRescheduled(Guid Id, DateTime? IssueDate);
