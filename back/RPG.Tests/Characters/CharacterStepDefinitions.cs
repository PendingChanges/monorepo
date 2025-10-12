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

    [Then("The character's level should be {int}")]
    public void ThenTheCharactersLevelShouldBe(int expectedLevel)
    {
        Assert.Equal(expectedLevel, _characterContext.Character?.Level);
    }

    [Then("The character's strength should be {int}")]
    public void ThenTheCharactersStrengthShouldBe(int expectedStrength)
    {
        Assert.Equal(expectedStrength, _characterContext.Character?.Strength);
    }

    [Then("The character's precision should be {int}")]
    public void ThenTheCharactersPrecisionShouldBe(int expectedPrecision)
    {
        Assert.Equal(expectedPrecision, _characterContext.Character?.Precision);
    }

    [Then("The character's intelligence should be {int}")]
    public void ThenTheCharactersIntelligenceShouldBe(int expectedIntelligence)
    {
        Assert.Equal(expectedIntelligence, _characterContext.Character?.Intelligence);
    }

    [Then("The character's hp should be {int}")]
    public void ThenTheCharactersHpShouldBe(int expectedHp)
    {
        Assert.Equal(expectedHp, _characterContext.Character?.HP);
    }

    [Then("The character's mp should be {int}")]
    public void ThenTheCharactersMpShouldBe(int expectedMp)
    {
        Assert.Equal(expectedMp, _characterContext.Character?.MP);
    }

    [Then("the character's defense should be {int}")]
    public void ThenTheCharactersDefenseShouldBe(int expectedDefense)
    {
        Assert.Equal(expectedDefense, _characterContext.Character?.Defense);
    }

    [Then("the character's spirit should be {int}")]
    public void ThenTheCharactersSpiritShouldBe(int expectedSpirit)
    {
        Assert.Equal(expectedSpirit, _characterContext.Character?.Spirit);
    }

    [Then("the character's evasion should be {int}")]
    public void ThenTheCharactersEvasionShouldBe(int expectedEvasion)
    {
        Assert.Equal(expectedEvasion, _characterContext.Character?.Evasion);
    }

    [Then("the character's speed should be {int}")]
    public void ThenTheCharactersSpeedShouldBe(int expectedSpeed)
    {
        Assert.Equal(expectedSpeed, _characterContext.Character?.Speed);
    }

}
