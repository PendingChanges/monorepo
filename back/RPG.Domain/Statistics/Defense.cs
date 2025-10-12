namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the defense statistic of a character.
/// </summary>
public readonly record struct Defense
{
    public int Value { get; init; }

    public Defense(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Defense cannot be negative.");
        
        Value = value;
    }

    public static Defense Zero => new(0);

    public static implicit operator int(Defense defense) => defense.Value;
    public static explicit operator Defense(int value) => new(value);

    public Defense Add(int amount) => new(Value + amount);
    public Defense Subtract(int amount) => new(Math.Max(0, Value - amount));
}
