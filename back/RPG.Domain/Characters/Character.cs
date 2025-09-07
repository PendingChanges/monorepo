
namespace RPG.Domain.Characters;

public record Character(string Name, ExperienceTable ExperienceTable, int ExperiencePoints = 0)
{
    public Character? Rename(string newName) 
        => this with { Name = newName };

    public Character GainExperience(int points) 
        => this with { ExperiencePoints = ExperiencePoints + points };

    public int GetLevel() 
        => ExperienceTable.GetLevel(ExperiencePoints);
}
