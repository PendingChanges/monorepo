namespace Journalist.Crm.Domain.Contacts.DataModels;

public record class ContactDocument(Guid Id, Name Name, string OwnerId, List<Guid> ClientsIds);
