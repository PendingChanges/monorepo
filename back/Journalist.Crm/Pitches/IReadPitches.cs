using Domain.Common;
using Journalist.Crm.Domain.Pitches.DataModels;

namespace Journalist.Crm.Domain.Pitches;

public interface IReadPitches
{
    Task<PitchDocument?> GetPitchAsync(Guid id, string userId, CancellationToken cancellationToken = default);
    Task<PitchResultSet> GetPitchesAsync(ContextFilteredPaginatedRequest<GetPitchesRequest> request, CancellationToken cancellationToken = default);
}
