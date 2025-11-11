using BetterNote.Application.Tags;
using BetterNote.Domain.Tags.Events;
using Marten;
using Marten.Events.Projections;

namespace BetterNote.Infrastructure.Marten.Tags;
public class TagProjection : EventProjection
{
    public static TagDocument Create(TagCreated tagCreated)
        => new(tagCreated.TagId, tagCreated.Value);

    public static void Project(TagDeleted @event, IDocumentOperations ops)
    {
        ops.Delete<TagDocument>(@event.TagId);
        ops.DeleteWhere<TaggingDocument>(t => t.TagId == @event.TagId);
    }
}
