
using BetterNote.Domain.Tags;

namespace BetterNote.Application.Tags;
public interface IReadTags
{
    Task<IReadOnlyList<TagDocument>> GetAllTagAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<(Guid, TagDocument)>> GetTagsBySubjectIds(IReadOnlyList<Guid> keys, CancellationToken cancellationToken = default);
}
