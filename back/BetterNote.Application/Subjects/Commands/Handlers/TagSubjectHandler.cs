using BetterNote.Domain.Subjects;
using CQRS;

namespace BetterNote.Application.Subjects.Commands.Handlers;
internal sealed class TagSubjectHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<TagSubject, Subject>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Subject aggregate, TagSubject command, string ownerId)
        => aggregate.Tag(command.TagId);

    protected override Task<Subject?> LoadAggregate(TagSubject command, string ownerId, CancellationToken cancellationToken) 
        => AggregateReader.LoadAsync<Subject>(command.SubjectId, cancellationToken: cancellationToken);
}
