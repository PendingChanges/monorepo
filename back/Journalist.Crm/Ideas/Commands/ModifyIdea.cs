using CQRS;

namespace Journalist.Crm.Domain.Ideas.Commands;

public record ModifyIdea(Guid Id, string NewName, string? NewDescription) : ICommand;
