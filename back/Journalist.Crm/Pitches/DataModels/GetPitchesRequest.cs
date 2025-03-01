using Domain.Common;

namespace Journalist.Crm.Domain.Pitches.DataModels;

public record GetPitchesRequest(
     Guid? clientId,
     Guid? ideaId,
    int? skip,
    int? take,
    string? sortBy,
    string? sortDirection) : PaginatedRequestBase(
        skip ?? Constants.DefaultPageNumber,
        take ?? Constants.DefaultPageSize,
        sortBy ?? Constants.DefaultPitchSortBy,
        sortDirection ?? Constants.DefaultSortDirection)
{
    public Guid? ClientId { get; } = clientId;

    public Guid? IdeaId { get; set; } = ideaId;
}
