using Domain.Common;

namespace BetterNote.Application.Subjects;
public record GetSubjectsRequest(
    int? skip,
    int? take,
    string? sortBy,
    string? sortDirection) : PaginatedRequestBase(
        skip ?? Constants.DefaultPageNumber,
        take ?? Constants.DefaultPageSize,
        sortBy ?? Constants.DefaultSubjectSortBy,
        sortDirection ?? Constants.DefaultSortDirection);
