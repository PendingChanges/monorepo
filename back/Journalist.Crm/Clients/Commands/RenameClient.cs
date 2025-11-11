using CQRS;

namespace Journalist.Crm.Domain.Clients.Commands;

public sealed record RenameClient(Guid Id, string NewName) : ICommand;
