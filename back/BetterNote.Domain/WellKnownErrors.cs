namespace BetterNote;
public static class WellKnownErrors
{
    public const string TagAlreadyDeleted = "TAG_ALREADY_DELETED";

    internal static readonly Dictionary<string, string> Messages = new()
        {
            { TagAlreadyDeleted, "The tag has already been deleted"},
        };
}
