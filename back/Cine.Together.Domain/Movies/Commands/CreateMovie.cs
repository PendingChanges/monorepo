using CQRS;

namespace Cine.Together.Domain.Movies.Commands;

public record CreateMovie(string Name, DateOnly ReleaseDate, string LanguageCode) : ICommand;
