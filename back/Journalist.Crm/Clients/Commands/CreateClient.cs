using CQRS;

namespace Journalist.Crm.Domain.Clients.Commands;

public sealed record CreateClient(string Name) : ICommand;
