using Domain.Common;
using Journalist.Crm.Domain.Contacts;
using Journalist.Crm.Domain.Contacts.DataModels;
using Marten;
using Marten.Pagination;

namespace Journalist.Crm.Marten.Contacts;
public class ContactRepository(IQuerySession session) : IReadContacts
{
    private readonly IQuerySession _session = session;

    public async Task<ContactResultSet> GetContactsAsync(ContextFilteredPaginatedRequest<GetContactsRequest> request, CancellationToken cancellationToken = default)
    {
        var query = _session.Query<ContactDocument>().Where(c => c.OwnerId == request.UserId);

        if (request.PaginatedRequest.ClientId != null)
        {
            query = query.Where(c => c.ClientsIds.Any(p => p == request.PaginatedRequest.ClientId));
        }

        query = SortBy(request.PaginatedRequest, query);

        var pagedResult = await query.ToPagedListAsync(request.PaginatedRequest.Skip, request.PaginatedRequest.Take, cancellationToken);

        return new ContactResultSet([.. pagedResult], (int)pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
    }

    private static IQueryable<ContactDocument> SortBy(GetContactsRequest request, IQueryable<ContactDocument> query) => request.SortDirection switch
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
