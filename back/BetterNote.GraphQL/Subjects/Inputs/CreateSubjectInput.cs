namespace BetterNote.Infrastructure.GraphQL.Subjects.Inputs;
public sealed record CreateSubjectInput(string Title, string Description, IReadOnlyCollection<Guid>? TagsId);
