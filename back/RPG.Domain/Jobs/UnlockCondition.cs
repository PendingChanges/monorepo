namespace RPG.Domain.Jobs;

public sealed record UnlockCondition
{
    public string JobName { get; init; }
    public int LevelRequired { get; init; }

    public UnlockCondition(string jobName, int levelRequired)
    {
        JobName = jobName;
        LevelRequired = levelRequired;
    }

    /// <summary>
    /// Vérifie si la condition est remplie par rapport à un job donné
    /// </summary>
    public bool IsSatisfiedBy(Job job)
        => job.Name.Equals(JobName, StringComparison.OrdinalIgnoreCase) && job.Level >= LevelRequired;
}
