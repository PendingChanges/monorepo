using Domain.Common;

namespace Journalist.Crm.Domain.Contacts.DataModels;
public record ContactResultSet(
    IReadOnlyList<ContactDocument> data,
    int totalItemCount,
    bool hasNextPage,
    bool hasPreviousPage) : ResultSetBase<ContactDocument>(
        data,
        totalItemCount,
        hasNextPage,
        hasPreviousPage)
{
}
