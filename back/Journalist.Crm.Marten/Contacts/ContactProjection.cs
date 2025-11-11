using Journalist.Crm.Domain.Contacts.DataModels;
using Journalist.Crm.Domain.Contacts.Events;
using Marten.Events.Projections;

namespace Journalist.Crm.Marten.Contacts;

public class ContactProjection : EventProjection
{
    public static ContactDocument Create(ContactCreated contactCreated)
    => new(contactCreated.Id, contactCreated.Name, contactCreated.OwnerId, []);
}
