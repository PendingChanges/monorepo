using Cine.Together.Domain.Movies.DataModels;
using Cine.Together.Domain.Movies.Events;
using Marten.Events.Projections;

namespace Cine.Together.Marten.Movies;

public class MovieProjection : EventProjection
{
    public static MovieDocument Create(MovieCreated movieCreated)
        => new(movieCreated.Id, movieCreated.Name, movieCreated.ReleaseDate, movieCreated.LanguageCode);
}
