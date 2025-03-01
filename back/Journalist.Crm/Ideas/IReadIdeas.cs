using Domain.Common;
using Journalist.Crm.Domain.Ideas.DataModels;

namespace Journalist.Crm.Domain.Ideas;

public interface IReadIdeas
{
    Task<IReadOnlyList<IdeaDocument>> AutoCompleteIdeaAsync(string text, string userId, CancellationToken cancellationToken);
    Task<IdeaDocument?> GetIdeaAsync(Guid ideaId, string userId, CancellationToken cancellationToken = default);
    Task<IdeaResultSet> GetIdeasAsync(ContextFilteredPaginatedRequest<GetIdeasRequest> request, CancellationToken cancellationToken = default);
}
