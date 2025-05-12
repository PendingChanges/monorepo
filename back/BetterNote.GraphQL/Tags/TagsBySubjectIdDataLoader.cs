using BetterNote.Application.Tags;
using GreenDonut;

namespace BetterNote.Infrastructure.Marten.Tags;
public class TagsBySubjectIdDataLoader : GroupedDataLoader<Guid, TagDocument>
{
    private readonly IReadTags _tagReader;

    public TagsBySubjectIdDataLoader(
        IReadTags tagReader,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options)
        : base(batchScheduler, options)
    {
        _tagReader = tagReader;
    }

    protected override async Task<ILookup<Guid, TagDocument>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var res = await _tagReader.GetTagsBySubjectIds(keys, cancellationToken);
        return res.ToLookup(t => t.Item1, t => t.Item2);
    }
}