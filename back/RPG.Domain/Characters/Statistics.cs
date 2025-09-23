namespace RPG.Domain.Characters;
public sealed record Statistics
{
    private StatisticGainTable _statisticGainTable;
    private int _strength;
    private int _precision;
    int _hp;
    int _mp;
    int _defense;
    int _magic;
    int _spirit;
    int _evasion;
    int _speed;

    private Statistics(StatisticGainTable statisticGainTable, int strength, int precision, int hp, int mp, int defense, int magic, int spirit, int evasion, int speed)
    {
        _statisticGainTable = statisticGainTable;
        _strength = strength;
        _precision = precision;
        _hp = hp;
        _mp = mp;
        _defense = defense;
        _magic = magic;
        _spirit = spirit;
        _evasion = evasion;
        _speed = speed;
    }

    public static Statistics New() => new(StatisticGainTable.Empty(), 0, 0, 0, 0, 0, 0, 0, 0, 0);
}