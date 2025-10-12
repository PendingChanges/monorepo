namespace RPG.Domain.Statistics;

/// <summary>
/// Represents the magic power statistic of a character.
/// </summary>
public readonly record struct Magic
{
    public int Value { get; init; }

    public Magic(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Magic cannot be negative.");
        
        Value = value;
    }

    public static Magic Zero => new(0);

    public static implicit operator int(Magic magic) => magic.Value;
    public static explicit operator Magic(int value) => new(value);

    public Magic Add(int amount) => new(Value + amount);
    public Magic Subtract(int amount) => new(Math.Max(0, Value - amount));
}
