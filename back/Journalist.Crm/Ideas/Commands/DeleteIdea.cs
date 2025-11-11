using CQRS;

namespace Journalist.Crm.Domain.Ideas.Commands;

public sealed record DeleteIdea(Guid Id) : ICommand;
