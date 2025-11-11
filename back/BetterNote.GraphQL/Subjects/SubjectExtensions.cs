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
#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
    public async Task<IEnumerable<TagModel>> GetTagsAsync(
#pragma warning restore S2325 // Methods and properties that don't access instance data should be static
        [Parent] SubjectModel subject,
        [Service] TagsBySubjectIdDataLoader tagsBySubjectIdDataLoader,
        [Service] IContext context,
        CancellationToken cancellationToken = default)
    {
        var tags = await tagsBySubjectIdDataLoader.LoadAsync(subject.Id, cancellationToken);

        return (tags ?? []).MapToTagModels();
    }
}
