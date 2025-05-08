using CQRS;

namespace BetterNote.Application.Tags.Commands;

public sealed record DeleteTag(Guid Id) : ICommand;