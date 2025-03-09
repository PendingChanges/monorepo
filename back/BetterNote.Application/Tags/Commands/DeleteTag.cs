using CQRS;

namespace BetterNote.Tags.Commands;

public record DeleteTag(Guid Id) : ICommand;