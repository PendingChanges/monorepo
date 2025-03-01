using Domain.Common;

namespace Doc.Management.Documents.DataModels;

public record GetDocumentsRequest(
    int? skip,
    int? take,
    string? sortBy,
    string? sortDirection) : PaginatedRequestBase(
    skip ?? Constants.DefaultPageNumber,
    take ?? Constants.DefaultPageSize,
    sortBy ?? Constants.DefaultDocumentSortBy,
    sortDirection ?? Constants.DefaultSortDirection)
{
}
