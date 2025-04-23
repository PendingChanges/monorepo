using System;
using BetterNote.Domain.Tags;
using BetterNote.Domain.Tags.Events;
using Journalist.Crm.UnitTests.Domain;
using Reqnroll;
using Xunit;

namespace BetterNote.UnitTests;

[Binding]
public class TagStepDefinitions(AggregateContext aggregateContext)
{
    private readonly AggregateContext _aggregateContext = aggregateContext;

    [Given("No existing tag")]
    public void GivenNoExistingTag()
    {
       // Noting
    }

    [When("I create a tag with value {string}")]
    public void WhenICreateATagWithValue(string pouet)
    {
        var aggregate = new Tag();
        _aggregateContext.Aggregate = aggregate;
        _aggregateContext.Result = aggregate.CreateTag(pouet);
    }

    [Then("A tag with value {string} is created")]
    public void ThenATagWithValueIsCreated(string pouet)
    {
        var tag = _aggregateContext.Aggregate as Tag;
        Assert.NotNull(tag);
        Assert.Equal(pouet, tag.Value);
       
        var events = _aggregateContext.GetEvents().ToList();
        Assert.Single(events);
        var @event = events.LastOrDefault() as TagCreated;
        Assert.NotNull(@event);
        Assert.Equal(pouet, @event.Value);
        Assert.Equal(tag.Id, @event.TagId);
    }

    [Given("A tag with value {string}")]
    public void GivenATagWithValue(string value)
    {
        var tag = new Tag();
        tag.CreateTag(value);
        _aggregateContext.Aggregate = tag;
    }

    [When("I delete the tag with value {string}")]
    public void WhenIDeleteTheTagWithValue(string value)
    {
        var aggregate = _aggregateContext.Aggregate as Tag;
        Assert.NotNull(aggregate);
        _aggregateContext.Result = aggregate.Delete();
    }

    [Then("The tag with value {string} is deleted")]
    public void ThenTheTagWithValueIsDeleted(string value)
    {
        var tag = _aggregateContext.Aggregate as Tag;
        Assert.NotNull(tag);
        Assert.True(tag.Deleted);

    }

}
