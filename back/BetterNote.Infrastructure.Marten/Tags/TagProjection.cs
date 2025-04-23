using BetterNote.Application.Tags;
using BetterNote.Domain.Tags.Events;
using Marten;
using Marten.Events.Projections;

namespace BetterNote.Infrastructure.Marten.Tags;
public class TagProjection : EventProjection
{
    public TagDocument Create(TagCreated tagCreated)
        => new(tagCreated.TagId, tagCreated.Value);

    public void Project(TagDeleted @event, IDocumentOperations ops)
     => ops.Delete<TagDocument>(@event.TagId);
}
