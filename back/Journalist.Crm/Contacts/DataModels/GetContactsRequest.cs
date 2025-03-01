using Domain.Common;

namespace Journalist.Crm.Domain.Contacts.DataModels;

public record GetContactsRequest(
    Guid? clientId,
    int? skip,
    int? take,
    string? sortBy,
    string? sortDirection) : PaginatedRequestBase(
    skip ?? Constants.DefaultPageNumber,
    take ?? Constants.DefaultPageSize,
    sortBy ?? Constants.DefaultClientSortBy,
    sortDirection ?? Constants.DefaultSortDirection)
{
    public Guid? ClientId { get; set; } = clientId;
}
