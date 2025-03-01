using CQRS;

namespace Journalist.Crm.Domain.Contacts.Commands;

public record CreateContact(Name Name) : ICommand;