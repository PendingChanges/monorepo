using Domain.Common;

namespace Journalist.Crm.Domain.Pitches.DataModels;

public record PitchResultSet(IReadOnlyList<PitchDocument> data, int totalItemCount, bool hasNextPage, bool hasPreviousPage) : ResultSetBase<PitchDocument>(data, totalItemCount, hasNextPage, hasPreviousPage)
{
}
