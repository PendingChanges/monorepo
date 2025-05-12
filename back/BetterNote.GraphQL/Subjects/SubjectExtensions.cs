using BetterNote.Application.Tags;
using BetterNote.Infrastructure.GraphQL.Tags;
using BetterNote.Infrastructure.Marten.Tags;
using CQRS;
using HotChocolate;
using HotChocolate.Types;

namespace BetterNote.Infrastructure.GraphQL.Subjects;

[ExtendObjectType(typeof(SubjectModel))]
public class SubjectExtensions
{
    public async Task<IEnumerable<TagModel>> GetTagsAsync(
        [Parent] SubjectModel pitch,
        [Service] TagsBySubjectIdDataLoader tagsBySubjectIdDataLoader,
        [Service] IContext context,
        CancellationToken cancellationToken = default)
    {
        var tags = await tagsBySubjectIdDataLoader.LoadAsync(pitch.Id, cancellationToken);

        return (tags ?? []).MapToTagModels();
    }
}
