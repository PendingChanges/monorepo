using CQRS;

namespace Journalist.Crm.Domain.Pitches.Commands;

public sealed record CreatePitch(PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, Guid ClientId, Guid IdeaId) : ICommand;
