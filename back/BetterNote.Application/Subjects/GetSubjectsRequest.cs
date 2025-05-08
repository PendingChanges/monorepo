using Domain.Common;

namespace BetterNote.Application.Subjects;
public sealed record GetSubjectsRequest(
    int? skip,
    int? take,
    string? SortBy,
    string? SortDirection) : PaginatedRequestBase(
        skip ?? Constants.DefaultPageNumber,
        take ?? Constants.DefaultPageSize,
        SortBy ?? Constants.DefaultSubjectSortBy,
        SortDirection ?? Constants.DefaultSortDirection);
