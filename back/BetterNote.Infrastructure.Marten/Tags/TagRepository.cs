using BetterNote.Application.Tags;
using Marten;

namespace BetterNote.Infrastructure.Marten.Tags;
public class TagRepository(IQuerySession session) : IReadTags
{
    public Task<IReadOnlyList<TagDocument>> GetAllTagAsync(CancellationToken cancellationToken = default)
    => session.Query<TagDocument>().ToListAsync(cancellationToken);

    public async Task<IEnumerable<(Guid, TagDocument)>> GetTagsBySubjectIds(IReadOnlyList<Guid> keys, CancellationToken cancellationToken = default)
    {
        var dict = new Dictionary<Guid, TagDocument>();
        await session.Query<TaggingDocument>().Include(dict).On(s => s.TagId).Where(s => keys.Contains(s.Id)).ToListAsync();

        return dict.Select(k => (k.Key, k.Value));
    }
}
