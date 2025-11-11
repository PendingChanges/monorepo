namespace Journalist.Crm.Domain.Pitches.Events;

public sealed record PitchClientChanged(Guid Id, Guid ClientId);
