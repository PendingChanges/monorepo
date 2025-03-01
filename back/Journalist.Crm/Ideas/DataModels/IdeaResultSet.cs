using Domain.Common;

namespace Journalist.Crm.Domain.Ideas.DataModels;

public record IdeaResultSet(IReadOnlyList<IdeaDocument> data, int totalItemCount, bool hasNextPage, bool hasPreviousPage) : ResultSetBase<IdeaDocument>(data, totalItemCount, hasNextPage, hasPreviousPage)
{
}
