namespace Cine.Together.Domain.Movies.DataModels;

public sealed record MovieDocument(Guid Id, string Name, DateOnly ReleaseDate, string LanguageCode);
