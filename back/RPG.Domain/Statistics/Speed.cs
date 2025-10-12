namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the speed statistic of a character.
/// </summary>
public readonly record struct Speed
{
    public int Value { get; init; }

    public Speed(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Speed cannot be negative.");
        
        Value = value;
    }

    public static Speed Zero => new(0);

    public static implicit operator int(Speed speed) => speed.Value;
    public static explicit operator Speed(int value) => new(value);

    public Speed Add(int amount) => new(Value + amount);
    public Speed Subtract(int amount) => new(Math.Max(0, Value - amount));
}
