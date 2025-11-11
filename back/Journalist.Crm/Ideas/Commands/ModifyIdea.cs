using CQRS;

namespace Journalist.Crm.Domain.Ideas.Commands;

public sealed record ModifyIdea(Guid Id, string NewName, string? NewDescription) : ICommand;
