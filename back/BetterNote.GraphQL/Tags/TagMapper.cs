using BetterNote.Application.Tags;

namespace BetterNote.Infrastructure.GraphQL.Tags;
internal static class TagMapper
{
    public static TagModel? MapToTagModelOrNull(this TagDocument? tagDocument) => tagDocument != null ? tagDocument.MapToTagModel() : null;

    public static TagModel MapToTagModel(this TagDocument tagDocument) => new TagModel(tagDocument.Id, tagDocument.Value);

    public static IReadOnlyList<TagModel> MapToTagModels(this IReadOnlyCollection<TagDocument> tagDocuments) => tagDocuments.Select(MapToTagModel).ToList();
}
