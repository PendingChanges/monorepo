using CQRS;

namespace Journalist.Crm.Domain.Clients.Commands;

public record RenameClient(Guid Id, string NewName) : ICommand;
