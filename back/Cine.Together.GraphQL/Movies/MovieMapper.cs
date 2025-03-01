using Cine.Together.Domain.Movies.DataModels;

namespace Cine.Together.GraphQL.Movies;

public static class MovieMapper
{
    public static Movie? ToMovieOrNull(this MovieDocument? clientDocument)
     => clientDocument?.ToMovie();

    public static Movie ToMovie(this MovieDocument clientDocument)
        => new(clientDocument.Id, clientDocument.Name, clientDocument.ReleaseDate, clientDocument.LanguageCode);

    public static Cine.Together.Domain.Movies.Commands.CreateMovie ToCommand(this CreateMovie createMovie)
       => new(createMovie.Name, createMovie.ReleaseDate, createMovie.LanguageCode);
}
