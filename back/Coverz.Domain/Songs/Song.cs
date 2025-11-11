using CQRS;

namespace Coverz.Domain.Songs;

internal sealed class Song : Aggregate
{
    private readonly ICollection<Guid> _existingArtistIds;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Song(ICollection<Guid> existingArtistIds)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _existingArtistIds = existingArtistIds;
    }

    public string Name { get; private set;  }

    public Guid ArtistId { get; private set; }

    public AggregateResult CreateSong(string Name, Guid ArtistId)
    {
        var result = AggregateResult.Create();

        result.CheckAndAddError(
            () => string.IsNullOrWhiteSpace(Name),
            WellKnownErrors.NameIsRequired());

        result.CheckAndAddError(
            () => !_existingArtistIds.Contains(ArtistId),
            WellKnownErrors.ArtistDoesNotExist(ArtistId));

        if (result.HasErrors)
        {
            return result;
        }

        var id = Guid.NewGuid();
        var @event = new SongCreated(id, Name, ArtistId);

        Apply(@event);
        result.AddEvent(@event);
        return result;
    }

    private void Apply(SongCreated @event)
    {
        SetId(@event.SongId);

        Name = @event.Name;
        ArtistId = @event.ArtistId;

        IncrementVersion();
    }
}
