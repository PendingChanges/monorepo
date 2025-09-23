using RPG.Domain.Characters;

namespace RPG.Domain.Parties;
public sealed record Party(IEnumerable<Character> Members)
{
    public Party AddMember(Character member) => this with { Members = Members.Append(member) };
}
