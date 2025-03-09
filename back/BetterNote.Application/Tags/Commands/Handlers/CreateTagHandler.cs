using BetterNote.Domain.Tags;
using CQRS;

namespace BetterNote.Tags.Commands.Handlers;
internal class CreateTagHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<CreateTag, Tag>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Tag aggregate, CreateTag command, string ownerId) 
        => aggregate.CreateTag(command.Value);

    protected override Task<Tag?> LoadAggregate(CreateTag command, string ownerId, CancellationToken cancellationToken)
        => Task.FromResult<Tag?>(new Tag());
}
