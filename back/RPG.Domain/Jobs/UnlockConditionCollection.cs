namespace RPG.Domain.Jobs;

public class UnlockConditionCollection
{
    private readonly List<UnlockCondition> _conditions = new();

    public IReadOnlyList<UnlockCondition> Conditions => _conditions.AsReadOnly();

    public UnlockConditionCollection()
    {
    }

    public UnlockConditionCollection(params UnlockCondition[] conditions)
    {
        _conditions.AddRange(conditions);
    }

    public void Add(UnlockCondition condition)
    {
        _conditions.Add(condition);
    }

    public bool IsEmpty() => _conditions.Count == 0;
}
