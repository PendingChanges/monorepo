using CQRS;

namespace Journalist.Crm.Domain.Pitches.Commands;

public sealed record ModifyPitch(Guid Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, Guid ClientId, Guid IdeaId) : ICommand;
