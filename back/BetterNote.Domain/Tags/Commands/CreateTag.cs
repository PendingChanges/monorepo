using CQRS;

namespace BetterNote.Tags.Commands;
public record CreateTag(string Value) : ICommand;
