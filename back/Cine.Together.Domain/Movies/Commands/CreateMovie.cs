using CQRS;

namespace Cine.Together.Domain.Movies.Commands;

public sealed record CreateMovie(string Name, DateOnly ReleaseDate, string LanguageCode) : ICommand;
