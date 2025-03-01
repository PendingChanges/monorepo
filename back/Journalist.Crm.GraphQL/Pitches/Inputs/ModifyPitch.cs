using Journalist.Crm.Domain.Pitches;

namespace Journalist.Crm.GraphQL.Pitches.Inputs;

public record ModifyPitch(Guid Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, Guid ClientId, Guid IdeaId);
