using CQRS;

namespace Journalist.Crm.Domain.Clients.Commands;

public sealed record DeleteClient(Guid Id) : ICommand;
