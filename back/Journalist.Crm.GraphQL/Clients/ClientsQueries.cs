using Domain.Common;
using HotChocolate.Authorization;
using HotChocolate.Types.Pagination;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.DataModels;
using Client = Journalist.Crm.GraphQL.Clients.Outputs.Client;
using CQRS;
using HotChocolate.Types;
using HotChocolate;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType("Query")]
public class ClientsQueries
{
    [Authorize(Roles = ["user"])]
    [GraphQLName("allClients")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Client>> GetClientsAsync(
        [Service] IReadClients clientReader,
           [Service] IContext context,
            int? skip,
            int? take,
            string? sortBy,
            string? sortDirection,
            CancellationToken cancellationToken = default
        )
    {
        var request = new GetClientsRequest(null, skip, take, sortBy, sortDirection);
        var contextedRequest = new ContextFilteredPaginatedRequest<GetClientsRequest>(request, context.UserId);
        var clientResultSet = await clientReader.GetClientsAsync(contextedRequest, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(clientResultSet.HasNextPage, clientResultSet.HasPreviousPage);

        var collectionSegment = new CollectionSegment<Client>(
            clientResultSet.Data.ToClients(),
            pageInfo,
            clientResultSet.TotalItemCount);

        return collectionSegment;
    }

    [Authorize(Roles = ["user"])]
    [GraphQLName("client")]
    public async Task<Client?> GetClientAsync(
        [Service] IReadClients clientReader,
        [Service] IContext context,
        Guid id,
        CancellationToken cancellationToken = default)
        => (await clientReader.GetClientAsync(id, context.UserId, cancellationToken)).ToClientOrNull();

    [Authorize(Roles = ["user"])]
    [GraphQLName("autoCompleteClient")]
    public async Task<IReadOnlyList<Client>> AutoCompleteClientAsync(
        [Service] IReadClients clientReader,
        [Service] IContext context,
        string text,
        CancellationToken cancellationToken = default)
        => (await clientReader.AutoCompleteClientAsync(text, context.UserId, cancellationToken)).ToClients();
}
