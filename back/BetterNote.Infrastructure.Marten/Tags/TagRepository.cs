using BetterNote.Application.Tags;
using Marten;

namespace BetterNote.Infrastructure.Marten.Tags;
public class TagRepository(IQuerySession session) : IReadTags
{
    public Task<IReadOnlyList<TagDocument>> GetAllTagAsync(CancellationToken cancellationToken = default)
    => session.Query<TagDocument>().ToListAsync(cancellationToken);
}
