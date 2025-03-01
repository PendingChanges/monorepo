namespace Journalist.Crm.Domain.Clients.Events;

public sealed record ClientRenamed(Guid Id, string NewName);
