using CQRS;

namespace BetterNote.Application.Subjects.Commands;
public sealed record CreateSubject(string Title, string Description, IReadOnlyCollection<Guid> TagsId) : ICommand;
