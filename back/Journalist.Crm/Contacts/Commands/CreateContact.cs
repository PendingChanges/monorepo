using CQRS;

namespace Journalist.Crm.Domain.Contacts.Commands;

public sealed record CreateContact(Name Name) : ICommand;