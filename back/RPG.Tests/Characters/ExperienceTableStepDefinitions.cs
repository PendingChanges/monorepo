
using Reqnroll;
using RPG.Domain.Characters;
using Xunit;

namespace RPG.Tests.Characters;

[Binding]
public sealed class ExperienceTableStepDefinitions(ExperienceTableContext experienceTableContext)
{
    private readonly ExperienceTableContext _experienceTableContext = experienceTableContext;

    [Given("A experience table with the following levels:")]
    public void GivenAExperienceTableWithTheFollowingLevels(DataTable dataTable)
    {
        _experienceTableContext.ExperienceTable = ExperienceTable.FromDictionary(
            dataTable.Rows.ToDictionary(
                row => int.Parse(row["Level"]),
                row => int.Parse(row["ExperiencePoints"])
            )
        );
    }

    [When("I ask for the level corresponding to {int} experience points")]
    public void WhenIAskForTheLevelCorrespondingToExperiencePoints(int experiencePoints)
    {
        _experienceTableContext.AskedExperiencePoints = experiencePoints;
    }

    [Then("I should get level {int}")]
    public void ThenIShouldGetLevel(int level)
    {
        Assert.Equal(level, _experienceTableContext.ExperienceTable!.GetLevel(_experienceTableContext.AskedExperiencePoints));
    }

    [When("I ask for the experience points required for next level from experience points {int}")]
    public void WhenIAskForTheExperiencePointsRequiredForNextLevelFromExperiencePoints(int experiencePoints)
    {
        _experienceTableContext.AskedExperiencePoints = experiencePoints;
    }

    [Then("I should get {int} experience points")]
    public void ThenIShouldGetExperiencePoints(int experiencePointsRemaining)
    {
        Assert.Equal(experiencePointsRemaining, _experienceTableContext.ExperienceTable!.GetExperienceForNextLevel(_experienceTableContext.AskedExperiencePoints));
    }
}
