namespace Journalist.Crm.Domain.Pitches.DataModels;

public record PitchDocument(Guid Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, Guid ClientId, Guid IdeaId, string OwnerId, string StatusCode);
