using RPG.Domain.Statistics;

namespace RPG.Domain.Characters;

public sealed record StatisticTable
{
    private StatisticTable(Strength strength, Precision precision, HP hp, MP mp, Defense defense, Magic intelligence, Spirit spirit, Evasion evasion, Speed speed)
    {
        Strength = strength;
        Precision = precision;
        HP = hp;
        MP = mp;
        Defense = defense;
        Intelligence = intelligence;
        Spirit = spirit;
        Evasion = evasion;
        Speed = speed;
    }

    public Strength Strength { get; }
    public Precision Precision { get; }
    public HP HP { get; }
    public MP MP { get; }
    public Defense Defense { get; }
    public Magic Intelligence { get; }
    public Spirit Spirit { get; }
    public Evasion Evasion { get; }
    public Speed Speed { get; }

    public static StatisticTable New() => new(
        new Strength(10),
        new Precision(10),
        new HP(100),
        new MP(50),
        new Defense(5),
        new Magic(10),
        new Spirit(5),
        new Evasion(5),
        new Speed(5)
    );
}