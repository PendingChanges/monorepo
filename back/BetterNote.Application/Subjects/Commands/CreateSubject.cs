using CQRS;
using MediatR;

namespace BetterNote.Application.Subjects.Commands;
public record CreateSubject(string Title, string Description, IReadOnlyCollection<Guid> TagsId) : ICommand;
