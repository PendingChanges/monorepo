using HotChocolate;

namespace BetterNote.Infrastructure.GraphQL.Subjects;

[GraphQLName("Subject")]
public sealed record SubjectModel(Guid Id, string Title, string Description);
