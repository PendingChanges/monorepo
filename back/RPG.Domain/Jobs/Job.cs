using RPG.Domain.Skills;

namespace RPG.Domain.Jobs;

public sealed record Job
{
    private readonly string _name;
    private int _level;
    private int _totalJobPointsGained;
    private int _jobPointsUnspent;
    private bool _isUnlocked;
    private readonly SkillCollection _skillCollection = new();
    private readonly UnlockConditionCollection _unlockConditions;

    public string Name => _name;
    public int Level => _level;
    public int TotalJobPointsGained => _totalJobPointsGained;
    public int JobPointsUnspent => _jobPointsUnspent;
    public bool IsUnlocked => _isUnlocked;
    public UnlockConditionCollection UnlockConditions => _unlockConditions;
    public SkillCollection Skills => _skillCollection;

    private Job(string name, int level, int totalJobPointsGained, int jobPointsUnspent, bool isUnlocked, UnlockConditionCollection unlockConditions)
    {
        _name = name;
        _level = level;
        _totalJobPointsGained = totalJobPointsGained;
        _jobPointsUnspent = jobPointsUnspent;
        _isUnlocked = isUnlocked;
        _unlockConditions = unlockConditions;
    }

    public static Job New(string name, bool isUnlockedByDefault = false, UnlockConditionCollection? unlockConditions = null)
        => new(name, 1, 0, 0, isUnlockedByDefault, unlockConditions ?? new UnlockConditionCollection());

    public Job GainJobPoints(int points)
        => this with { _totalJobPointsGained = _totalJobPointsGained + points, _jobPointsUnspent = _jobPointsUnspent + points };

    public Job LevelUp()
        => this with { _level = _level + 1 };

    public Job Unlock()
        => this with { _isUnlocked = true };
}
