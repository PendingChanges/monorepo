namespace Cine.Together.GraphQL.Movies;

public record CreateMovie(string Name, DateOnly ReleaseDate, string LanguageCode);
