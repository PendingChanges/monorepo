using Domain.Common;
using HotChocolate.Authorization;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using CQRS;

namespace Journalist.Crm.GraphQL.Ideas;

[ExtendObjectType("Query")]
public class IdeasQueries
{
    [Authorize(Roles = ["user"])]
    [GraphQLName("allIdeas")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Outputs.Idea>> GetIdeas(
         [Service] IReadIdeas ideasReader,
         [Service] IContext context,
            int? skip,
            int? take,
            string? sortBy,
            string? sortDirection,
            CancellationToken cancellationToken = default)
    {
        var request = new GetIdeasRequest(null, skip, take, sortBy, sortDirection);
        var contextedRequest = new ContextFilteredPaginatedRequest<GetIdeasRequest>(request, context.UserId);
        var pitchesResultSet = await ideasReader.GetIdeasAsync(contextedRequest, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(pitchesResultSet.HasNextPage, pitchesResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Outputs.Idea>(
            pitchesResultSet.Data.ToIdeas(),
            pageInfo,
            pitchesResultSet.TotalItemCount);

        return collectionSegment;
    }


    [Authorize(Roles = ["user"])]
    [GraphQLName("idea")]
    public async Task<Outputs.Idea?> GetIdeaAsync([Service] IReadIdeas ideasReader, [Service] IContext context, Guid id, CancellationToken cancellationToken = default)
        => (await ideasReader.GetIdeaAsync(id, context.UserId, cancellationToken)).ToIdeaOrNull();

    [Authorize(Roles = ["user"])]
    [GraphQLName("autoCompleteIdea")]
    public async Task<IReadOnlyList<Outputs.Idea>> AutoCompleteIdeaAsync(
    [Service] IReadIdeas ideaReader,
    [Service] IContext context,
    string text,
    CancellationToken cancellationToken = default)
    => (await ideaReader.AutoCompleteIdeaAsync(text, context.UserId, cancellationToken)).ToIdeas();
}
