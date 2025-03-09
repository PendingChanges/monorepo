using BetterNote.Domain.Subjects;
using CQRS;

namespace BetterNote.Application.Subjects.Commands.Handlers;
internal class CreateSubjectHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<CreateSubject, Subject>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Subject aggregate, CreateSubject command, string ownerId)
        => aggregate.CreateSubject(command.Title, command.Description, command.TagsId);

    protected override Task<Subject?> LoadAggregate(CreateSubject command, string ownerId, CancellationToken cancellationToken) => Task.FromResult<Subject?>(new Subject());
}
