using Reqnroll;
using RPG.Domain.Characters;

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

    [When("I ask for the level corresponding to {string} experience points")]
    public void WhenIAskForTheLevelCorrespondingToExperiencePoints(string p0)
    {
        throw new PendingStepException();
    }

    [Then("I should get level {string}")]
    public void ThenIShouldGetLevel(string p0)
    {
        throw new PendingStepException();
    }
}
