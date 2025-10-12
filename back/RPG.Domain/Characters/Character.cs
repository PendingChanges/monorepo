using RPG.Domain.Jobs;

namespace RPG.Domain.Characters;

public sealed record Character
{
    private string _name;
    private readonly int _level;
    private readonly StatisticTable _statisticTable;
    private readonly JobCollection _jobCollection;

    public string Name => _name;
    public int Level => _level;
    public JobCollection Jobs => _jobCollection;
    
    // Expose statistics from StatisticTable
    public int Strength => _statisticTable.Strength.Value;
    public int Precision => _statisticTable.Precision.Value;
    public int Intelligence => _statisticTable.Intelligence.Value;
    public int HP => _statisticTable.HP.Value;
    public int MP => _statisticTable.MP.Value;
    public int Defense => _statisticTable.Defense.Value;
    public int Spirit => _statisticTable.Spirit.Value;
    public int Evasion => _statisticTable.Evasion.Value;
    public int Speed => _statisticTable.Speed.Value;

    private Character(string name, int level, StatisticTable statisticTable, JobCollection jobCollection)
    {
        _name = name;
        _level = level;
        _statisticTable = statisticTable;
        _jobCollection = jobCollection;
    }

    public Character? Rename(string newName)
        => this with { _name = newName };

    public static Character CreateNew(string name)
        => new(name, 1, StatisticTable.New(), DefaultJobFactory.CreateDefaultJobs());
}
