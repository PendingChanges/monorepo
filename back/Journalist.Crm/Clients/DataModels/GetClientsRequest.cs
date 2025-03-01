using Domain.Common;

namespace Journalist.Crm.Domain.Clients.DataModels;

public record GetClientsRequest(
    Guid? PitchId,
    int? skip,
    int? take,
    string? sortBy,
    string? sortDirection) : PaginatedRequestBase(
    skip ?? Constants.DefaultPageNumber,
    take ?? Constants.DefaultPageSize,
    sortBy ?? Constants.DefaultClientSortBy,
    sortDirection ?? Constants.DefaultSortDirection);
