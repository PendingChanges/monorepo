using CQRS;

namespace Journalist.Crm.Domain.Pitches.Commands;

public record CreatePitch(PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, Guid ClientId, Guid IdeaId) : ICommand;
