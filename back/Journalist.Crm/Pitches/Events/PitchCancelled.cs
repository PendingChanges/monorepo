namespace Journalist.Crm.Domain.Pitches.Events;

public sealed record PitchCancelled(Guid Id, Guid ClientId, Guid IdeaId);
