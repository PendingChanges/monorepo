namespace Journalist.Crm.Domain.Ideas.Events;

public sealed record IdeaCreated(Guid Id, string Name, string? Description, string OwnerId);
