using Journalist.Crm.Domain.Contacts;
using Journalist.Crm.GraphQL.Contacts.Inputs;

namespace Journalist.Crm.GraphQL.Contacts;
public static class ContactMapper
{
    public static Domain.Contacts.Commands.CreateContact ToCommand(this AddContact addContact)
    => new(new Name(addContact.FirstName, addContact.Lastname));
}
