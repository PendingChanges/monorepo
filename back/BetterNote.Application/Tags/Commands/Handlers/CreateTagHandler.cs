using BetterNote.Domain.Tags;
using CQRS;

namespace BetterNote.Application.Tags.Commands.Handlers;
internal sealed class CreateTagHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader, IReadTags tagReader) : SingleAggregateCommandHandler<CreateTag, Tag>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Tag aggregate, CreateTag command, string ownerId)
        => aggregate.CreateTag(command.Value);

    protected override async Task<Tag?> LoadAggregate(CreateTag command, string ownerId, CancellationToken cancellationToken)
    {
        var tags = await tagReader.GetAllTagAsync(cancellationToken);
        var existingTagValues = tags.Select(x => x.Value).ToList();
        return new Tag(existingTagValues);
    }
}
