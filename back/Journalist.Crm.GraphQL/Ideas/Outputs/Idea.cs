namespace Journalist.Crm.GraphQL.Ideas.Outputs;

public record Idea(Guid Id, string Name, string? Description, string UserId, int NbOfPitches);