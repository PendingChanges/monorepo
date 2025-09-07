using RPG.Domain.Characters;
using RPG.Domain.Parties;

namespace RPG.Tests.Parties;
public sealed class PartyContext
{
    public Character? MemberAddedToParty { get; set; }
    public Party? Party { get; set; }
}
