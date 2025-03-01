using CQRS;

namespace Journalist.Crm.Domain;
internal static class WellKnownErrors
{
    public static Error NotIdeaOwner() => new ("NOT_IDEA_OWNER", "The user is not the owner");

    internal static Error NotClientOwner()
    {
        throw new NotImplementedException();
    }

    internal static Error NotPitchOwner()
    {
        throw new NotImplementedException();
    }

    internal static Error PitchNotAcceptable()
    {
        throw new NotImplementedException();
    }

    internal static Error PitchNotCancellable()
    {
        throw new NotImplementedException();
    }

    internal static Error PitchNotModifiable()
    {
        throw new NotImplementedException();
    }

    internal static Error PitchNotRefusable()
    {
        throw new NotImplementedException();
    }

    internal static Error PitchNotSendable()
    {
        throw new NotImplementedException();
    }

    internal static Error PitchNotValidatable()
    {
        throw new NotImplementedException();
    }
}
