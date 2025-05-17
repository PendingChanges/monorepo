using MediatR;

namespace BetterNote.Application.Subjects.Queries.Handlers;
internal sealed class GetSubjectByIdHandler(IReadSubjects subjectReader) : IRequestHandler<GetSubjectById, SubjectDocument?>
{
    public Task<SubjectDocument?> Handle(GetSubjectById query, CancellationToken cancellationToken) => subjectReader.GetSubjectAsync(query.Id, cancellationToken);
}
