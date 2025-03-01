namespace Journalist.Crm.GraphQL.Ideas.Inputs;

public record ModifyIdea(Guid Id, string NewName, string? NewDescription);
