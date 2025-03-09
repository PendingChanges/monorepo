namespace BetterNote.Application.Subjects;
public interface IReadSubjects
{
    Task<SubjectResultSet> GetSubjectsAsync(GetSubjectsRequest request, CancellationToken cancellationToken = default);
}
