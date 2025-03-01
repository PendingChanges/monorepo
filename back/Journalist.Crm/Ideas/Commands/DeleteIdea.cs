using CQRS;

namespace Journalist.Crm.Domain.Ideas.Commands;

public record DeleteIdea(Guid Id) : ICommand;
