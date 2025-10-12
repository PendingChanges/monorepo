namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the precision/accuracy statistic of a character.
/// </summary>
public readonly record struct Precision
{
    public int Value { get; init; }

    public Precision(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Precision cannot be negative.");
        
        Value = value;
    }

    public static Precision Zero => new(0);

    public static implicit operator int(Precision precision) => precision.Value;
    public static explicit operator Precision(int value) => new(value);

    public Precision Add(int amount) => new(Value + amount);
    public Precision Subtract(int amount) => new(Math.Max(0, Value - amount));
}
