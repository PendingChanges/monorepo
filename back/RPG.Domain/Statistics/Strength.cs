namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the strength statistic of a character.
/// </summary>
public readonly record struct Strength
{
    public int Value { get; init; }

    public Strength(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Strength cannot be negative.");
        
        Value = value;
    }

    public static Strength Zero => new(0);

    public static implicit operator int(Strength strength) => strength.Value;
    public static explicit operator Strength(int value) => new(value);

    public Strength Add(int amount) => new(Value + amount);
    public Strength Subtract(int amount) => new(Math.Max(0, Value - amount));
}
