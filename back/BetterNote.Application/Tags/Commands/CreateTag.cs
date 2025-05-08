using CQRS;

namespace BetterNote.Application.Tags.Commands;
public sealed record CreateTag(string Value) : ICommand;
