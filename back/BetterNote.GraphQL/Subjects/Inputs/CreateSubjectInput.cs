namespace BetterNote.Infrastructure.GraphQL.Subjects.Inputs;
public record CreateSubjectInput(string Title, string Description, IReadOnlyCollection<Guid>? TagsId);
