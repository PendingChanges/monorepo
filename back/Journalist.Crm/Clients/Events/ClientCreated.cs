namespace Journalist.Crm.Domain.Clients.Events;

public sealed record ClientCreated(Guid Id, string Name, string OwnerId);
