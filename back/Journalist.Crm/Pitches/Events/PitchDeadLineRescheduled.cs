namespace Journalist.Crm.Domain.Pitches.Events;

public sealed record PitchDeadLineRescheduled(Guid Id, DateTime? DeadLineDate);
