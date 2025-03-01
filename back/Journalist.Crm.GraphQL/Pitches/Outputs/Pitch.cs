using Journalist.Crm.Domain.Pitches;

namespace Journalist.Crm.GraphQL.Pitches.Outputs;

public record Pitch(Guid Id,
    PitchContent Content,
    DateTime? DeadLineDate,
    DateTime? IssueDate,
    Guid ClientId,
    Guid IdeaId,
    string UserId);
