using Cine.Together.Domain.Movies.DataModels;

namespace Cine.Together.Domain.Movies;

public interface IReadMovies
{
    Task<MovieDocument?> GetMovieAsync(Guid movieId, CancellationToken cancellationToken = default);
}
