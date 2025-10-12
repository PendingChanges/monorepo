namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the health points (HP) statistic of a character.
/// </summary>
public readonly record struct HP
{
    public int Value { get; init; }

    public HP(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "HP cannot be negative.");
        
        Value = value;
    }

    public static HP Zero => new(0);

    public static implicit operator int(HP hp) => hp.Value;
    public static explicit operator HP(int value) => new(value);

    public HP Add(int amount) => new(Value + amount);
    public HP Subtract(int amount) => new(Math.Max(0, Value - amount));
}
