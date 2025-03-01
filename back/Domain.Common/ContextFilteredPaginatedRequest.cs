namespace Domain.Common;

public record ContextFilteredPaginatedRequest<T>(T PaginatedRequest, string UserId) where T : PaginatedRequestBase;
