using BetterNote.Application.Tags;
using GreenDonut;

namespace BetterNote.Infrastructure.Marten.Tags;
public class TagsBySubjectIdDataLoader(
    IReadTags tagReader,
    IBatchScheduler batchScheduler,
    DataLoaderOptions options) : GroupedDataLoader<Guid, TagDocument>(batchScheduler, options)
{
    private readonly IReadTags _tagReader = tagReader;

    protected override async Task<ILookup<Guid, TagDocument>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var res = await _tagReader.GetTagsBySubjectIds(keys, cancellationToken);
        return res.ToLookup(t => t.Item1, t => t.Item2);
    }
}