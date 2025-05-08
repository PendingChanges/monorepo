using Domain.Common;

namespace BetterNote.Application.Subjects;
public sealed record SubjectResultSet(IReadOnlyList<SubjectDocument> Data, int TotalItemCount, bool HasNextPage, bool HasPreviousPage)
    : ResultSetBase<SubjectDocument>(Data, TotalItemCount, HasNextPage, HasPreviousPage);
