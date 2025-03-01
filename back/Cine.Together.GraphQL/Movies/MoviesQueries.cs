using Cine.Together.Domain.Movies;
using CQRS;
using HotChocolate.Authorization;

namespace Cine.Together.GraphQL.Movies;

[ExtendObjectType("Query")]
public class MoviesQueries
{
    [Authorize(Roles = ["user"])]
    [GraphQLName("movie")]
    public async Task<Movie?> GetMovieAsync(
        [Service] IReadMovies moviesReader,
        [Service] IContext context,
        Guid id,
        CancellationToken cancellationToken = default)
        => (await moviesReader.GetMovieAsync(id, cancellationToken)).ToMovieOrNull();
}
