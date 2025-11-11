using Coverz.Domain.Songs;
using Reqnroll;
using System;
using Tests.Common;
using Xunit;

namespace Coverz.UnitTests.Domain.Songs;

[Binding]
public class SongStepDefinitions(AggregateContext aggregateContext)
{
    private readonly AggregateContext _aggregateContext = aggregateContext;

    [Given("No existing tag")]
    public void GivenNoExistingTag()
    {
        // Nothing
    }

    [When("I create a song with name {string} and artist id {string}")]
    public void WhenICreateASongWithNameAndArtistId(string name, string artistId)
    {
        var artistIdGuid = Guid.Parse(artistId);
        var existingArtistIds = new[] { artistIdGuid };
        var aggregate = new Song(existingArtistIds);
        _aggregateContext.Aggregate = aggregate;
        _aggregateContext.Result = aggregate.CreateSong(name, artistIdGuid);
    }

    [Then("A song with name {string} and artist id {string} is created")]
    public void ThenASongWithNameAndArtistIdIsCreated(string name, string artistId)
    {
        var song = _aggregateContext.Aggregate as Song;
        var artistIdGuid = Guid.Parse(artistId);
        Assert.NotNull(song);
        Assert.Equal(name, song.Name);
        Assert.Equal(artistIdGuid, song.ArtistId);

        var events = _aggregateContext.GetEvents().ToList();
        Assert.Single(events);
        var @event = events.LastOrDefault() as SongCreated;
        Assert.NotNull(@event);
        Assert.Equal(name, @event.Name);
        Assert.Equal(song.Id, @event.SongId);
        Assert.Equal(artistIdGuid, @event.ArtistId);
    }
}
