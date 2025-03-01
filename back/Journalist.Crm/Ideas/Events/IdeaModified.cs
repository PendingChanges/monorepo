namespace Journalist.Crm.Domain.Ideas.Events;

public sealed record IdeaModified(Guid Id, string NewName, string? NewDescription);
