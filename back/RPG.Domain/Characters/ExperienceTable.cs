
namespace RPG.Domain.Characters;
public class ExperienceTable
{
    private readonly IDictionary<int, int> _experienceLevels;

    private ExperienceTable(IDictionary<int, int> experienceLevels) 
    { 
        _experienceLevels = experienceLevels;
    }

    public int GetLevel(int experiencePoints)
        => _experienceLevels.Where(kvp => kvp.Value <= experiencePoints)
                   .OrderByDescending(kvp => kvp.Key)
                   .Select(kvp => kvp.Key)
                   .FirstOrDefault();

    public int GetExperienceForNextLevel(int experiencePoints)
    {
        var currentLevel = GetLevel(experiencePoints);
        
        if (!_experienceLevels.ContainsKey(currentLevel + 1))
        {
            return 0;
        }

        return _experienceLevels[currentLevel + 1] - experiencePoints;
    }

    public static ExperienceTable Empty() => new(new Dictionary<int, int>());
    public static ExperienceTable FromDictionary(IDictionary<int, int> experienceLevels) => new(experienceLevels);
}
