namespace Domain.Common;

public abstract record PaginatedRequestBase(int Skip, int Take, string SortBy, string SortDirection);
