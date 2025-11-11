namespace Journalist.Crm.Domain.Pitches.Events;

public sealed record PitchContentChanged(Guid Id, PitchContent Content);
