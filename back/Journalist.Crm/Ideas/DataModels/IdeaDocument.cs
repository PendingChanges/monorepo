namespace Journalist.Crm.Domain.Ideas.DataModels;

public record IdeaDocument(Guid Id, string Name, string? Description, string OwnerId, List<Guid> PitchesIds);