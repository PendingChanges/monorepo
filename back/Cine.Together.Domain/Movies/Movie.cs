using Cine.Together.Domain.Movies.Events;
using CQRS;

namespace Cine.Together.Domain.Movies;

public sealed class Movie : Aggregate
{
    public string Name { get; private set; }
    public DateOnly ReleaseDate { get; private set; }
    public string LanguageCode { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Movie() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public AggregateResult Create(string name, DateOnly releaseDate, string languageCode)
    {
        var result = AggregateResult.Create();

        var id = Guid.NewGuid();

        var @event = new MovieCreated(id, name, releaseDate, languageCode);

        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    private void Apply(MovieCreated @event)
    {
        SetId(@event.Id);

        Name = @event.Name;
        ReleaseDate = @event.ReleaseDate;
        LanguageCode = @event.LanguageCode;

        IncrementVersion();
    }
}
