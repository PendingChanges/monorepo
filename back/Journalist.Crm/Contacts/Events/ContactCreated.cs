namespace Journalist.Crm.Domain.Contacts.Events;

public sealed record ContactCreated(Guid Id, Name Name, string OwnerId);