using Domain.Common;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.DataModels;
using Marten;
using Marten.Pagination;

namespace Journalist.Crm.Marten.Ideas;

public class IdeaRepository(IQuerySession session) : IReadIdeas
{
    private readonly IQuerySession _session = session;

    public Task<IReadOnlyList<IdeaDocument>> AutoCompleteIdeaAsync(string text, string userId, CancellationToken cancellationToken)
        => _session.Query<IdeaDocument>().Where(c => c.OwnerId == userId && c.Name.Contains(text, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);

    public Task<IdeaDocument?> GetIdeaAsync(Guid ideaId, string userId, CancellationToken cancellationToken = default)
        => _session.Query<IdeaDocument>().Where(c => c.Id == ideaId && c.OwnerId == userId).FirstOrDefaultAsync(cancellationToken);

    public async Task<IdeaResultSet> GetIdeasAsync(ContextFilteredPaginatedRequest<GetIdeasRequest> request, CancellationToken cancellationToken = default)
    {
        var query = _session.Query<IdeaDocument>().Where(c => c.OwnerId == request.UserId);

        if (request.PaginatedRequest.PitchId != null)
        {
            query = query.Where(c => c.PitchesIds.Any(p => p == request.PaginatedRequest.PitchId));
        }

        query = SortBy(request.PaginatedRequest, query);

        var pagedResult = await query.ToPagedListAsync(request.PaginatedRequest.Skip, request.PaginatedRequest.Take, cancellationToken);

        return new IdeaResultSet([.. pagedResult], (int)pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
    }

    private static IQueryable<IdeaDocument> SortBy(GetIdeasRequest request, IQueryable<IdeaDocument> query) => request.SortDirection switch
    {
        "desc" => request.SortBy switch
        {
            _ => query.OrderByDescending(c => c.Name)
        },
        _ => request.SortBy switch
        {
            _ => query.OrderBy(c => c.Name)
        },
    };
}
