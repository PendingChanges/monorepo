namespace Domain.Common;

public abstract record ResultSetBase<T>(IReadOnlyList<T> Data, int TotalItemCount, bool HasNextPage, bool HasPreviousPage);
