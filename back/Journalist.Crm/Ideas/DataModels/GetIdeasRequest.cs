using Domain.Common;

namespace Journalist.Crm.Domain.Ideas.DataModels;

public record GetIdeasRequest(
     Guid? pitchId,
    int? skip,
    int? take,
    string? sortBy,
    string? sortDirection) : PaginatedRequestBase(
        skip ?? Constants.DefaultPageNumber,
        take ?? Constants.DefaultPageSize,
        sortBy ?? Constants.DefaultIdeaSortBy,
        sortDirection ?? Constants.DefaultSortDirection)
{
    public Guid? PitchId { get; } = pitchId;
}
