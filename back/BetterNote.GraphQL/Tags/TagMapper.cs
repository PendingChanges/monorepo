using BetterNote.Application.Tags;

namespace BetterNote.Infrastructure.GraphQL.Tags;
internal static class TagMapper
{
    public static TagModel? MapToTagModelOrNull(this TagDocument? tagDocument) => tagDocument?.MapToTagModel();

    public static TagModel MapToTagModel(this TagDocument tagDocument) => new(tagDocument.Id, tagDocument.Value);

    public static IEnumerable<TagModel> MapToTagModels(this IEnumerable<TagDocument> tagDocuments) => tagDocuments.Select(MapToTagModel);
}
