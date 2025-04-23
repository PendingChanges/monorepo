using BetterNote.Application.Tags;

namespace BetterNote.Infrastructure.GraphQL.Tags;
internal static class TagMapper
{
    public static TagModel? MapToTagModelOrNull(this TagDocument? tagDocument) => tagDocument?.MapToTagModel();

    public static TagModel MapToTagModel(this TagDocument tagDocument) => new(tagDocument.Id, tagDocument.Value);

    public static IReadOnlyList<TagModel> MapToTagModels(this IReadOnlyCollection<TagDocument> tagDocuments) => tagDocuments.Select(MapToTagModel).ToList();
}
