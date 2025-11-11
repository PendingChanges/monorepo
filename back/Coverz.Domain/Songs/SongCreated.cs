
namespace Coverz.Domain.Songs;

public sealed record SongCreated(Guid SongId, string Name, Guid ArtistId);
