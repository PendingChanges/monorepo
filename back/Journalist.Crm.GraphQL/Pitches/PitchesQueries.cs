using Domain.Common;
using HotChocolate.Authorization;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using Pitch = Journalist.Crm.GraphQL.Pitches.Outputs.Pitch;
using CQRS;
using HotChocolate.Types;
using HotChocolate;

namespace Journalist.Crm.GraphQL.Pitches;

[ExtendObjectType("Query")]
public class PitchesQueries
{
    [Authorize(Roles = ["user"])]
    [GraphQLName("allPitches")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Pitch>> GetPitches(
         [Service] IReadPitches pitchesReader,
         [Service] IContext context,
            Guid? clientId,
            Guid? ideaId,
            int? skip,
    int? take,
    string? sortBy,
    string? sortDirection,
            CancellationToken cancellationToken = default)
    {
        var request = new GetPitchesRequest(clientId, ideaId, skip, take, sortBy, sortDirection);
        var contextedRequest = new ContextFilteredPaginatedRequest<GetPitchesRequest>(request, context.UserId);
        var pitchesResultSet = await pitchesReader.GetPitchesAsync(contextedRequest, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(pitchesResultSet.HasNextPage, pitchesResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Pitch>(
            pitchesResultSet.Data.ToPitches(),
            pageInfo,
            pitchesResultSet.TotalItemCount);

        return collectionSegment;
    }


    [Authorize(Roles = ["user"])]
    [GraphQLName("pitch")]
    public async Task<Pitch?> GetPitchAsync([Service] IReadPitches pitchesReader, [Service] IContext context, Guid id, CancellationToken cancellationToken = default)
        => (await pitchesReader.GetPitchAsync(id, context.UserId, cancellationToken)).ToPitchOrNull();
}
