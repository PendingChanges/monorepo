using CQRS;
using HotChocolate.Authorization;
using MediatR;

namespace Cine.Together.GraphQL.Movies;

[ExtendObjectType("Mutation")]
internal class MoviesMutations
{
    [Authorize(Roles = ["user"])]
    [Error(typeof(DomainException))]
    [GraphQLName("addMovie")]
    public async Task<MovieAddedPayload> AddMovieAsync(
[Service] IMediator mediator,
[Service] IContext context,
CreateMovie createMovie,
CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Cine.Together.Domain.Movies.Commands.CreateMovie, Cine.Together.Domain.Movies.Movie>(createMovie.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new MovieAddedPayload { MovieId = result.Id };
    }
}
