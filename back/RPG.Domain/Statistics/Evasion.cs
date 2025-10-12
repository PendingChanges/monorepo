namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the evasion statistic of a character.
/// </summary>
public readonly record struct Evasion
{
    public int Value { get; init; }

    public Evasion(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Evasion cannot be negative.");
        
        Value = value;
    }

    public static Evasion Zero => new(0);

    public static implicit operator int(Evasion evasion) => evasion.Value;
    public static explicit operator Evasion(int value) => new(value);

    public Evasion Add(int amount) => new(Value + amount);
    public Evasion Subtract(int amount) => new(Math.Max(0, Value - amount));
}
