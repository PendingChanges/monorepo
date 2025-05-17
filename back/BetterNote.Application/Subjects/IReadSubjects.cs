
namespace BetterNote.Application.Subjects;
public interface IReadSubjects
{
    Task<SubjectDocument?> GetSubjectAsync(Guid id, CancellationToken cancellationToken);
    Task<SubjectResultSet> GetSubjectsAsync(GetSubjectsRequest request, CancellationToken cancellationToken = default);
}
