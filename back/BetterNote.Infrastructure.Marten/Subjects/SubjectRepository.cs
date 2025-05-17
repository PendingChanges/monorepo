using BetterNote.Application.Subjects;
using Marten;
using Marten.Pagination;

namespace BetterNote.Infrastructure.Marten.Subjects;
public sealed class SubjectRepository(IQuerySession session) : IReadSubjects
{
    public async Task<SubjectResultSet> GetSubjectsAsync(GetSubjectsRequest request, CancellationToken cancellationToken = default)
    {
        var query = session.Query<SubjectDocument>().AsQueryable();

        query = SortBy(request, query);

        var pagedResult = await query.ToPagedListAsync(request.Skip, request.Take, cancellationToken);

        return new SubjectResultSet([.. pagedResult], (int)pagedResult.TotalItemCount, pagedResult.HasNextPage, pagedResult.HasPreviousPage);
    }

    private static IQueryable<SubjectDocument> SortBy(GetSubjectsRequest request, IQueryable<SubjectDocument> query) => request.SortDirection switch
    {
        "desc" => request.SortBy switch
        {
            _ => query.OrderByDescending(c => c.Title)
        },
        _ => request.SortBy switch
        {
            _ => query.OrderBy(c => c.Title)
        },
    };

    public Task<SubjectDocument?> GetSubjectAsync(Guid id, CancellationToken cancellationToken) => session.Query<SubjectDocument>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}
