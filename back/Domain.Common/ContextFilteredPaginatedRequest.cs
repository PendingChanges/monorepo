namespace Domain.Common;

public sealed record ContextFilteredPaginatedRequest<T>(T PaginatedRequest, string UserId) where T : PaginatedRequestBase;
