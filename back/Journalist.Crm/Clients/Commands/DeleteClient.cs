using CQRS;

namespace Journalist.Crm.Domain.Clients.Commands;

public record DeleteClient(Guid Id) : ICommand;
