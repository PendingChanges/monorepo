using CQRS;

namespace Journalist.Crm.Domain;
internal static class WellKnownErrors
{
    public static DomainError NotIdeaOwner() => new ("NOT_IDEA_OWNER", "The user is not the owner");

    internal static DomainError NotClientOwner()
    {
        throw new NotImplementedException();
    }

    internal static DomainError NotPitchOwner()
    {
        throw new NotImplementedException();
    }

    internal static DomainError PitchNotAcceptable()
    {
        throw new NotImplementedException();
    }

    internal static DomainError PitchNotCancellable()
    {
        throw new NotImplementedException();
    }

    internal static DomainError PitchNotModifiable()
    {
        throw new NotImplementedException();
    }

    internal static DomainError PitchNotRefusable()
    {
        throw new NotImplementedException();
    }

    internal static DomainError PitchNotSendable()
    {
        throw new NotImplementedException();
    }

    internal static DomainError PitchNotValidatable()
    {
        throw new NotImplementedException();
    }
}
