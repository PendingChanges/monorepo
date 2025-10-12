namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the magic points (MP) statistic of a character.
/// </summary>
public readonly record struct MP
{
    public int Value { get; init; }

    public MP(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "MP cannot be negative.");
        
        Value = value;
    }

    public static MP Zero => new(0);

    public static implicit operator int(MP mp) => mp.Value;
    public static explicit operator MP(int value) => new(value);

    public MP Add(int amount) => new(Value + amount);
    public MP Subtract(int amount) => new(Math.Max(0, Value - amount));
}
