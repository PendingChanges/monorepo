using Domain.Common;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.DataModels;
using Marten;
using Marten.Pagination;

namespace Journalist.Crm.Marten.Pitches;

public class PitchRepository(IQuerySession session) : IReadPitches
{
    private readonly IQuerySession _session = session;

    public Task<PitchDocument?> GetPitchAsync(Guid id, string userId, CancellationToken cancellationToken = default)
        => _session.Query<PitchDocument>().Where(c => c.Id == id && c.OwnerId == userId).FirstOrDefaultAsync(cancellationToken);


    public async Task<PitchResultSet> GetPitchesAsync(ContextFilteredPaginatedRequest<GetPitchesRequest> request, CancellationToken cancellationToken = default)
    {
        var query = _session.Query<PitchDocument>().Where(p => p.OwnerId == request.UserId);

        if (request.PaginatedRequest.ClientId != null)
        {
            query = query.Where(p => p.ClientId == request.PaginatedRequest.ClientId);
        }

        if (request.PaginatedRequest.IdeaId != null)
        {
            query = query.Where(p => p.IdeaId == request.PaginatedRequest.IdeaId);
        }

        query = query.Where(p => p.StatusCode != PitchStates.Cancelled);

        query = SortBy(request.PaginatedRequest, query);

        var pagedResult = await query.ToPagedListAsync(request.PaginatedRequest.Skip, request.PaginatedRequest.Take, cancellationToken);

        return new PitchResultSet([.. pagedResult], (int)pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
    }

    private static IQueryable<PitchDocument> SortBy(GetPitchesRequest request, IQueryable<PitchDocument> query) => request.SortDirection switch
    {
        "desc" => request.SortBy switch
        {
            _ => query.OrderByDescending(c => c.Content.Title)
        },
        _ => request.SortBy switch
        {
            _ => query.OrderBy(c => c.Content.Title)
        },
    };
}
