using Domain.Common;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Marten;
using Marten.Pagination;

namespace Journalist.Crm.Marten.Clients;

public class ClientRepository(IQuerySession session) : IReadClients
{
    private readonly IQuerySession _session = session;

    public Task<IReadOnlyList<ClientDocument>> AutoCompleteClientAsync(string text, string userId, CancellationToken cancellationToken)
        => _session.Query<ClientDocument>().Where(c => c.OwnerId == userId && c.Name.Contains(text, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);

    public Task<ClientDocument?> GetClientAsync(Guid clientId, string userId, CancellationToken cancellationToken)
        => _session.Query<ClientDocument>().Where(c => c.Id == clientId && c.OwnerId == userId).FirstOrDefaultAsync(cancellationToken);

    public async Task<ClientResultSet> GetClientsAsync(ContextFilteredPaginatedRequest<GetClientsRequest> request, CancellationToken cancellationToken = default)
    {
        var query = _session.Query<ClientDocument>().Where(c => c.OwnerId == request.UserId);

        if (request.PaginatedRequest.PitchId != null)
        {
            query = query.Where(c => c.PitchesIds.Any(p => p == request.PaginatedRequest.PitchId));
        }
        query = SortBy(request.PaginatedRequest, query);

        var pagedResult = await query.ToPagedListAsync(request.PaginatedRequest.Skip, request.PaginatedRequest.Take, cancellationToken);

        return new ClientResultSet([.. pagedResult], (int)pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
    }

    private static IQueryable<ClientDocument> SortBy(GetClientsRequest request, IQueryable<ClientDocument> query) => request.SortDirection switch
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
