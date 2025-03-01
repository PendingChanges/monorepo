using Domain.Common;
using Journalist.Crm.Domain.Contacts.DataModels;

namespace Journalist.Crm.Domain.Contacts;

public interface IReadContacts
{
    Task<ContactResultSet> GetContactsAsync(ContextFilteredPaginatedRequest<GetContactsRequest> request, CancellationToken cancellationToken = default);
}
