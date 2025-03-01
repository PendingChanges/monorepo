﻿using CQRS;

namespace BetterNote.Tags.Commands.Handlers;
internal class DeleteTagHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<DeleteTag, Tag>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Tag aggregate, DeleteTag command, string ownerId)
        => aggregate.Delete();

    protected override Task<Tag?> LoadAggregate(DeleteTag command, string ownerId, CancellationToken cancellationToken)
        => AggregateReader.LoadAsync<Tag>(command.Id, cancellationToken: cancellationToken);
}
