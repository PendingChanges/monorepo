namespace Journalist.Crm.Domain.Pitches.Events;

public record PitchDeadLineRescheduled(Guid Id, DateTime? DeadLineDate);
