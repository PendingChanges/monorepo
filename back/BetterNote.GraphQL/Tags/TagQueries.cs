using BetterNote.Application.Tags.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace BetterNote.Infrastructure.GraphQL.Tags;

[ExtendObjectType("Query")]
public class TagQueries
{

#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
    public async Task<IEnumerable<TagModel>> GetAllTagsAsync(
#pragma warning restore S2325 // Methods and properties that don't access instance data should be static
        [Service] ISender sender, 
        CancellationToken cancellationToken = default)
        => (await sender.Send(new GetAllTags(), cancellationToken)).MapToTagModels();

}
