namespace Cine.Together.GraphQL.Movies;

public record Movie(Guid Id, string Name, DateOnly ReleaseDate, string LanguageCode);
