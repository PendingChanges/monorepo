namespace Cine.Together.Domain.Movies.Events;

public sealed record MovieCreated(Guid Id, string Name, DateOnly ReleaseDate, string LanguageCode);
