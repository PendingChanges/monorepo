namespace Cine.Together.Domain.Movies.DataModels;

public record MovieDocument(Guid Id, string Name, DateOnly ReleaseDate, string LanguageCode);
