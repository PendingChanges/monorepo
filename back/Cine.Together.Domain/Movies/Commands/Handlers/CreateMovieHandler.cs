using CQRS;

namespace Cine.Together.Domain.Movies.Commands.Handlers;

internal class CreateMovieHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<CreateMovie, Movie>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Movie aggregate, CreateMovie command, string ownerId) 
        => aggregate.Create(command.Name, command.ReleaseDate, command.LanguageCode);

    protected override Task<Movie?> LoadAggregate(CreateMovie command, string ownerId,
        CancellationToken cancellationToken) => Task.FromResult<Movie?>(new Movie());
}
