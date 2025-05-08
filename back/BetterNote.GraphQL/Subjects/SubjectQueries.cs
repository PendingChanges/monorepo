using BetterNote.Application.Subjects.Queries;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using MediatR;

namespace BetterNote.Infrastructure.GraphQL.Subjects;

[ExtendObjectType("Query")]
public sealed class SubjectQueries
{
    [GraphQLName("allSubjects")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<SubjectModel>> GetAllSubjectsAsync(
        [Service] ISender sender,
        int? skip,
        int? take,
        string? sortBy,
        string? sortDirection,
        CancellationToken cancellationToken = default)
    {
        var query = new GetSubjects(skip, take, sortBy, sortDirection);
        var result = await sender.Send(query, cancellationToken);
        var pageInfo = new CollectionSegmentInfo(result.HasNextPage, result.HasPreviousPage);
        return new CollectionSegment<SubjectModel>(result.Data.MapToSubjectModels(), pageInfo, result.TotalItemCount);
    }
}
