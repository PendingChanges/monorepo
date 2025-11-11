    
namespace RPG.Domain.Characters;
public sealed class ExperienceTable
{
    private readonly IDictionary<int, int> _experienceLevelsGaps;

    private ExperienceTable(IDictionary<int, int> experianceLevelsGaps) 
    { 
        _experienceLevelsGaps = experianceLevelsGaps;
    }

    public int GetLevel(int experiencePoints)
        => _experienceLevelsGaps.Where(kvp => kvp.Value <= experiencePoints)
                   .OrderByDescending(kvp => kvp.Key)
                   .Select(kvp => kvp.Key)
                   .FirstOrDefault();

    public int GetExperienceForNextLevel(int experiencePoints)
    {
        var currentLevel = GetLevel(experiencePoints);
        
        if (!_experienceLevelsGaps.ContainsKey(currentLevel + 1))
        {
            return 0;
        }

        return _experienceLevelsGaps[currentLevel + 1] - experiencePoints;
    }

    public static ExperienceTable Empty() => new(new Dictionary<int, int>());
    public static ExperienceTable FromDictionary(IDictionary<int, int> experienceLevels) => new(experienceLevels);
}
