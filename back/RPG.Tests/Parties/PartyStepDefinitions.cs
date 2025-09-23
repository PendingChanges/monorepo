using Reqnroll;
using RPG.Domain.Characters;
using Xunit;

namespace RPG.Tests.Parties;

[Binding]
public sealed class PartyStepDefinitions(PartyContext partyContext)
{
    private readonly PartyContext _partyContext = partyContext;

    [Given("A party with a member called {string}")]
    public void GivenAPartyWithAMemberCalled(string alreadyThere)
    {
        _partyContext.Party = new Domain.Parties.Party([Character.CreateNew(alreadyThere)]);
    }

    [When("I add a member {string} to the party")]
    public void WhenIAddAMemberToTheParty(string added)
    {
        _partyContext.MemberAddedToParty = Character.CreateNew(added);
        _partyContext.Party = _partyContext.Party?.AddMember(_partyContext.MemberAddedToParty);
    }

    [Then("The last member of the party should be {string}")]
    public void ThenTheLastMemberOfThePartyShouldBe(string added)
    {
        var last = _partyContext.Party?.Members.LastOrDefault();
        Assert.Equal(added, last?.Name);
    }
}
