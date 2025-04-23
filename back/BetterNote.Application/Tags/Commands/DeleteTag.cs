using CQRS;

namespace BetterNote.Application.Tags.Commands;

public record DeleteTag(Guid Id) : ICommand;