using RPG.Domain.Characters;

namespace RPG.Domain.Characters;

public sealed record CharacterNumbers
{
    private CharacterNumbers(ExperienceTable experienceTable, int experiencePoints, Statistics statistics)
    {
        _experienceTable = experienceTable;
        _experiencePoints = experiencePoints;
        _statistics = statistics;
    }

    private int _experiencePoints;
    private readonly Statistics _statistics;
    private readonly ExperienceTable _experienceTable;

    public int ExperiencePoints => _experiencePoints;

    internal CharacterNumbers GainExperience(int points) => this with { _experiencePoints = _experiencePoints + points };
    internal int GetLevel() => _experienceTable.GetLevel(ExperiencePoints);
    public static CharacterNumbers New() => new(ExperienceTable.Empty(), 0, Statistics.New());
}
