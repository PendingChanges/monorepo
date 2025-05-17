using CQRS;

namespace BetterNote.Application.Subjects.Commands;
public sealed record TagSubject(Guid SubjectId, Guid TagId) : ICommand;
