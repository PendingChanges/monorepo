using Domain.Common;

namespace Doc.Management.Documents.DataModels;

public record DocumentResultSet(
    IReadOnlyList<DocumentDocument> data,
    int totalItemCount,
    bool hasNextPage,
    bool hasPreviousPage) : ResultSetBase<DocumentDocument>(
        data,
        totalItemCount,
        hasNextPage,
        hasPreviousPage)
{
}
