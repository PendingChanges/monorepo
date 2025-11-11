using CQRS;

namespace Journalist.Crm.Domain.Ideas.Commands;

public sealed record CreateIdea(string Name, string? Description) : ICommand;
