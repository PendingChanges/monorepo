using CQRS;

namespace Coverz.Domain;

internal static class WellKnownErrors
{
    public static DomainError NameIsRequired() => new("NAME_IS_REQUIRED", "The song name is required");

    internal static DomainError ArtistDoesNotExist(Guid artistId) => new DomainError("ARTIST_DOES_NOT_EXIST", $"The artist with ID {artistId} does not exist");
}
