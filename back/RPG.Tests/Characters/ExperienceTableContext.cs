using RPG.Domain.Characters;

namespace RPG.Tests.Characters;
public sealed class ExperienceTableContext
{
    public ExperienceTable? ExperienceTable { get; set; }

    public int AskedExperiencePoints { get; set; }
}
