using Domain.Common;

namespace Journalist.Crm.Domain.Clients.DataModels;

public record ClientResultSet(
    IReadOnlyList<ClientDocument> data,
    int totalItemCount,
    bool hasNextPage,
    bool hasPreviousPage) : ResultSetBase<ClientDocument>(
        data,
        totalItemCount,
        hasNextPage,
        hasPreviousPage)
{
}
