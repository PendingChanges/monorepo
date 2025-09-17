
namespace RPG.Domain.Characters;

public record Character
{
    public Character(string name, ExperienceTable experienceTable, int experiencePoints)
    {
        Name = name;
        ExperienceTable = experienceTable;
        ExperiencePoints = experiencePoints;
    }

    public Character(string name, ExperienceTable experienceTable) : this(name, experienceTable, 0) { }

    public string Name { get; private set; }
    public ExperienceTable ExperienceTable { get; private set; }
    public int ExperiencePoints { get; private set; }

    public Character? Rename(string newName)
        => this with { Name = newName };

    public Character GainExperience(int points)
        => this with { ExperiencePoints = ExperiencePoints + points };

    public int GetLevel()
        => ExperienceTable.GetLevel(ExperiencePoints);
}
