namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the spirit/magic defense statistic of a character.
/// </summary>
public readonly record struct Spirit
{
    public int Value { get; init; }

    public Spirit(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Spirit cannot be negative.");
        
        Value = value;
    }

    public static Spirit Zero => new(0);

    public static implicit operator int(Spirit spirit) => spirit.Value;
    public static explicit operator Spirit(int value) => new(value);

    public Spirit Add(int amount) => new(Value + amount);
    public Spirit Subtract(int amount) => new(Math.Max(0, Value - amount));
}
