namespace Journalist.Crm.Domain.Contacts.DataModels;

public sealed record class ContactDocument(Guid Id, Name Name, string OwnerId, List<Guid> ClientsIds);
