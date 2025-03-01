using Domain.Common;
using Journalist.Crm.Domain.Clients.DataModels;

namespace Journalist.Crm.Domain.Clients;

public interface IReadClients
{
    Task<IReadOnlyList<ClientDocument>> AutoCompleteClientAsync(string text, string userId, CancellationToken cancellationToken);
    Task<ClientDocument?> GetClientAsync(Guid clientId, string userId, CancellationToken cancellationToken);
    Task<ClientResultSet> GetClientsAsync(ContextFilteredPaginatedRequest<GetClientsRequest> request, CancellationToken cancellationToken = default);
}
