namespace BetterNote.Domain.Tags.Events;
public sealed record TagCreated(Guid TagId, string Value);
