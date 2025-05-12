using BetterNote.Application.Tags.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace BetterNote.Infrastructure.GraphQL.Tags;

[ExtendObjectType("Query")]
public class TagQueries
{

    public async Task<IEnumerable<TagModel>> GetAllTagsAsync(
        [Service] ISender sender, 
        CancellationToken cancellationToken = default)
        => (await sender.Send(new GetAllTags(), cancellationToken)).MapToTagModels();

}
