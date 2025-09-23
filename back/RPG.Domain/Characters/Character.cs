namespace RPG.Domain.Characters;

public sealed record Character
{
    private string _name;
    private CharacterNumbers _numbers;

    public string Name => _name;
    public int ExperiencePoints => _numbers.ExperiencePoints;

    private Character(string name, CharacterNumbers numbers)
    {
        _name = name;
        _numbers = numbers;
    }

    public Character? Rename(string newName)
        => this with { _name = newName };

    public Character GainExperience(int points)
        => this with { _numbers = _numbers.GainExperience(points) };

    public int GetLevel() => _numbers.GetLevel();

    public static Character CreateNew(string name)
        => new(name, CharacterNumbers.New());
}
