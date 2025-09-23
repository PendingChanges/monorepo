using Reqnroll;
using RPG.Domain.Characters;
using System.Xml.Linq;
using Xunit;

namespace RPG.Tests.Characters;

[Binding]
public sealed class CharacterStepDefinitions(CharacterContext characterContext)
{
    private readonly CharacterContext _characterContext = characterContext;

    [Given("No existing character")]
    public static void GivenNoExistingCharacter()
    {
       // Nothing
    }

    [When("I create a character with the name {string}")]
    public void WhenICreateACharacterWithTheName(string name)
    {
        _characterContext.Character = Character.CreateNew(name);
    }

    [Then("The character's name should be {string}")]
    public void ThenTheCharacterNameSouhldBe(string name)
    {
        Assert.Equal(_characterContext.Character?.Name, name);
    }

    [Given("An existing character with the name {string}")]
    public void GivenAnExistingCharacterWithTheName(string oldName)
    {
        _characterContext.Character = Character.CreateNew(oldName);
    }

    [When("I rename the character to {string}")]
    public void WhenIRenameTheCharacterTo(string newName)
    {
        _characterContext.Character = _characterContext.Character?.Rename(newName);
    }

    [Given("An existing character with the name {string} and {int} experience points")]
    public void GivenAnExistingCharacterWithTheNameAndExperiencePoints(string name, int xpBase)
    {
        _characterContext.Character = Character.CreateNew(name)
                                            .GainExperience(xpBase);
    }

    [When("I add {int} experience points to the character")]
    public void WhenIAddExperiencePointsToTheCharacter(int xp)
    {
        _characterContext.Character = _characterContext.Character?.GainExperience(xp);
    }

    [Then("The character should have {int} experience points")]
    public void ThenTheCharacterShouldHaveExperiencePoints(int sum)
    {
        Assert.Equal(sum, _characterContext.Character?.ExperiencePoints);
    }

}
