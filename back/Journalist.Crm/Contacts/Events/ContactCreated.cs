namespace Journalist.Crm.Domain.Contacts.Events;

public record ContactCreated(Guid Id, Name Name, string OwnerId);