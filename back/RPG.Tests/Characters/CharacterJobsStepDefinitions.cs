using Reqnroll;
using RPG.Domain.Characters;
using RPG.Domain.Jobs;
using Xunit;

namespace RPG.Tests.Characters;

[Binding]
public sealed class CharacterJobsStepDefinitions(CharacterContext characterContext)
{
    private readonly CharacterContext _characterContext = characterContext;

    [Then("The character should have {int} jobs")]
    public void ThenTheCharacterShouldHaveJobs(int expectedCount)
    {
        Assert.Equal(expectedCount, _characterContext.Character?.Jobs.Jobs.Count);
    }

    [Then("The character should have a job called {string}")]
    public void ThenTheCharacterShouldHaveAJobCalled(string jobName)
    {
        Assert.True(_characterContext.Character?.Jobs.HasJob(jobName), 
            $"Character should have a job called '{jobName}'");
    }

    [Then("The job {string} should be unlocked")]
    public void ThenTheJobShouldBeUnlocked(string jobName)
    {
        var job = _characterContext.Character?.Jobs.GetJobByName(jobName);
        Assert.NotNull(job);
        Assert.True(job.IsUnlocked, $"Job '{jobName}' should be unlocked");
    }

    [Then("The job {string} should be locked")]
    public void ThenTheJobShouldBeLocked(string jobName)
    {
        var job = _characterContext.Character?.Jobs.GetJobByName(jobName);
        Assert.NotNull(job);
        Assert.False(job.IsUnlocked, $"Job '{jobName}' should be locked");
    }

    [Then("The job {string} should have {int} unlock condition(s)")]
    public void ThenTheJobShouldHaveUnlockConditions(string jobName, int expectedCount)
    {
        var job = _characterContext.Character?.Jobs.GetJobByName(jobName);
        Assert.NotNull(job);
        Assert.Equal(expectedCount, job.UnlockConditions.Conditions.Count);
    }

    [Then("The job {string} should require {string} at level {int}")]
    public void ThenTheJobShouldRequire(string jobName, string requiredJobName, int requiredLevel)
    {
        var job = _characterContext.Character?.Jobs.GetJobByName(jobName);
        Assert.NotNull(job);
        
        var condition = job.UnlockConditions.Conditions.FirstOrDefault(c => 
            c.JobName.Equals(requiredJobName, StringComparison.OrdinalIgnoreCase));
        
        Assert.NotNull(condition);
        Assert.Equal(requiredLevel, condition.LevelRequired);
    }
}
