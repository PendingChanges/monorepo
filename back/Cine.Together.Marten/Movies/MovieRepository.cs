using Cine.Together.Domain.Movies;
using Cine.Together.Domain.Movies.DataModels;
using Marten;

namespace Cine.Together.Marten.Movies;

internal class MovieRepository(IQuerySession session) : IReadMovies
{
    private readonly IQuerySession _session = session;

    public Task<MovieDocument?> GetMovieAsync(Guid movieId, CancellationToken cancellationToken = default)
        => _session.Query<MovieDocument>().Where(c => c.Id == movieId).FirstOrDefaultAsync(cancellationToken);
}
