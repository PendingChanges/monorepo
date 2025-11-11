namespace RPG.Domain.Skills;
public sealed class SkillCollection
{
    private readonly List<Skill> _skills = new List<Skill>();

    public IReadOnlyList<Skill> Skills => _skills.AsReadOnly();
}
