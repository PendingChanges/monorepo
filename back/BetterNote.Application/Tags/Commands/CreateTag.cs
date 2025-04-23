using CQRS;

namespace BetterNote.Application.Tags.Commands;
public record CreateTag(string Value) : ICommand;
