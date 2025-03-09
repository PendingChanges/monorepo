using HotChocolate;

namespace BetterNote.Infrastructure.GraphQL.Subjects;

[GraphQLName("Subject")]
public record SubjectModel(Guid Id, string Title, string Description);
