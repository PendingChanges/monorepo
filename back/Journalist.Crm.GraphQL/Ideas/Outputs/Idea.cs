namespace Journalist.Crm.GraphQL.Ideas.Outputs;

public sealed record Idea(Guid Id, string Name, string? Description, string UserId, int NbOfPitches);