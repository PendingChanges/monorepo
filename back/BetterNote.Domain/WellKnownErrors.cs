namespace BetterNote.Domain;
public static class WellKnownErrors
{
    public const string TagAlreadyDeleted = "TAG_ALREADY_DELETED";
    public const string TagAlreadyExists = "TAG_ALREADY_EXISTS";

    internal static readonly Dictionary<string, string> Messages = new()
        {
            { TagAlreadyDeleted, "The tag has already been deleted"},
            { TagAlreadyExists, "The tag already exists" }
        };
}
