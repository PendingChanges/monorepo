namespace RPG.Domain.Characters;
public sealed class StatisticGainTable
{
    private readonly IDictionary<int, Statistics> _statisticGains;
    private StatisticGainTable(IDictionary<int, Statistics> statisticGains)
    {
        _statisticGains = statisticGains;
    }
    public Statistics GetStatisticGainsForLevel(int level)
        => _statisticGains.TryGetValue(level, out Statistics? value) ? value : Statistics.New();
    public static StatisticGainTable Empty() => new(new Dictionary<int, Statistics>());
    public static StatisticGainTable FromDictionary(IDictionary<int, Statistics> statisticGains) => new(statisticGains);
}
