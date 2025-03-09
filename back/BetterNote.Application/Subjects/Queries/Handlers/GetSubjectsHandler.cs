using MediatR;

namespace BetterNote.Application.Subjects.Queries.Handlers;
internal class GetSubjectsHandler(IReadSubjects subjectReader) : IRequestHandler<GetSubjects, SubjectResultSet>
{
    public Task<SubjectResultSet> Handle(GetSubjects query, CancellationToken cancellationToken)
    {
        var request = new GetSubjectsRequest(query.Skip, query.Take, query.SortBy, query.SortDirection);
        return subjectReader.GetSubjectsAsync(request, cancellationToken);
    }
}
