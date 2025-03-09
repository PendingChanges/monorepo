using HotChocolate;

namespace BetterNote.Infrastructure.GraphQL.Tags;

[GraphQLName("Tag")]
public record TagModel(Guid Id, string Value);
