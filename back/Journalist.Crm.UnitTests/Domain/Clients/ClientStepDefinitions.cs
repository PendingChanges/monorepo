using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Events;
using TechTalk.SpecFlow;
using Xunit;

namespace Journalist.Crm.UnitTests.Domain.Clients;

[Binding]
public class ClientStepDefinitions(AggregateContext aggregateContext)
{

    private readonly AggregateContext _aggregateContext = aggregateContext;

    [Given(@"No existing client")]
    public void GivenNoExistingClient()
    {
        // Nothing to do more
    }

    [When(@"A user with id ""([^""]*)"" create a client with name ""([^""]*)""")]
    public void WhenAUserWithIdCreateAClientWithName(string ownerId, string name)
    {
        var aggregate = new Client();
        _aggregateContext.Result = aggregate.Create(name, ownerId);
        _aggregateContext.Aggregate = aggregate;
    }

    [Then(@"A client ""([^""]*)"" owned by ""([^""]*)"" is created")]
    public void ThenAClientOwnedByIsCreated(string name, string ownerId)
    {
        var clientAggregate = _aggregateContext.Aggregate as Client;
        Assert.NotNull(clientAggregate);
        Assert.Equal(name, clientAggregate.Name);
        Assert.Equal(ownerId, clientAggregate.OwnerId);

        var events = _aggregateContext.GetEvents();
        Assert.Single(events);
        var @event = events.LastOrDefault() as ClientCreated;

        Assert.NotNull(@event);
        Assert.Equal(name, @event.Name);
        Assert.Equal(ownerId, @event.OwnerId);
        Assert.Equal(clientAggregate.Id, @event.Id);
    }

    [Given(@"An existing client with name ""([^""]*)"" and an owner ""([^""]*)""")]
    public void GivenAnExistingClientWithNameAndAnOwner(string name, string ownerId)
    {
        var aggregate = new Client();
        aggregate.Create(name, ownerId);
        _aggregateContext.Aggregate = aggregate;
    }

    [When(@"A user with id ""([^""]*)"" delete the client")]
    public void WhenAUserWithIdDeleteTheClient(string ownerId)
    {
        var clientAggregate = _aggregateContext.Aggregate as Client;

        Assert.NotNull(clientAggregate);

        _aggregateContext.Result = clientAggregate.Delete(ownerId);
    }

    [Then(@"The client is deleted")]
    public void ThenTheClientIsDeleted()
    {
        var clientAggregate = _aggregateContext.Aggregate as Client;

        Assert.NotNull(clientAggregate);
        Assert.True(clientAggregate.Deleted);

        var events = _aggregateContext.GetEvents();
        Assert.Single(events);
        var @event = events.LastOrDefault() as ClientDeleted;

        Assert.NotNull(@event);
        Assert.Equal(clientAggregate.Id, @event.Id);
    }




    [Then(@"The client is not deleted")]
    public void ThenTheClientIsNotDeleted()
    {
        var clientAggregate = _aggregateContext.Aggregate as Client;

        Assert.NotNull(clientAggregate);
        Assert.False(clientAggregate.Deleted);

        Assert.DoesNotContain(_aggregateContext.GetEvents(), e => e is ClientDeleted);
    }

    [When(@"A user with id ""([^""]*)""rename the client to ""([^""]*)""")]
    public void WhenAUserWithIdRenameTheClientTo(string ownerId, string newName)
    {
        if (_aggregateContext.Aggregate is not Client clientAggregate)
        {
            return;
        }

        _aggregateContext.Result = clientAggregate.Rename(newName, ownerId);
    }

    [Then(@"The client is renamed to ""([^""]*)""")]
    public void ThenTheClientIsRenamedTo(string newName)
    {
        var clientAggregate = _aggregateContext.Aggregate as Client;

        Assert.NotNull(clientAggregate);
        Assert.Equal(newName, clientAggregate.Name);

        var events = _aggregateContext.GetEvents();
        Assert.Single(events);
        var @event = events.LastOrDefault() as ClientRenamed;

        Assert.NotNull(@event);
        Assert.Equal(clientAggregate.Id, @event.Id);
        Assert.Equal(newName, @event.NewName);
    }
}
