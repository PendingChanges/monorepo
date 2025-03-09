using BetterNote.Application.Subjects;

namespace BetterNote.Infrastructure.GraphQL.Subjects;
internal static class SubjectMapper
{
    public static SubjectModel? MapToSubjectModelOrNull(this SubjectDocument? subjectDocument) => subjectDocument != null ? subjectDocument.MapToSubjectModel() : null;

    public static SubjectModel MapToSubjectModel(this SubjectDocument subjectDocument) => new SubjectModel(subjectDocument.Id, subjectDocument.Title, subjectDocument.Description);

    public static IReadOnlyList<SubjectModel> MapToSubjectModels(this IReadOnlyCollection<SubjectDocument> SubjectDocuments) => SubjectDocuments.Select(MapToSubjectModel).ToList();
}
