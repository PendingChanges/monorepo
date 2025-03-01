using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Pitches;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using Moq;
using Xunit;
using CQRS;

namespace Journalist.Crm.UnitTests.CommandHandlers.Pitches;

public class DeletePitchHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReader;

    public DeletePitchHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReader = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_wrapped_command_delete_Pitch_properly()
    {
        //Arrange
        var ownerId = "user id";
        var pitchContent = new PitchContent("name", "content");
        var aggregate = new Pitch();
        aggregate.Create(pitchContent, DateTime.Now, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), ownerId);
        _aggregateReader.Setup(_ => _.LoadAsync<Pitch>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
        var handler = new DeletePitchHandler(_eventWriterMock.Object, _aggregateReader.Object);
        var command = new DeletePitch(aggregate.Id);
        var wrappedCommand = new WrappedCommand<DeletePitch, Pitch>(command, ownerId);

        //Act
        var aggregateInReturn = await handler.Handle(wrappedCommand, CancellationToken.None);

        //Assert
        _eventWriterMock.Verify(_ => _.StoreAsync(aggregateInReturn.Id, aggregateInReturn.Version, It.IsAny<IEnumerable<object>>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task Throw_domain_exception_when_aggregate_does_not_exists()
    {
        //Arrange
        var ownerId = "user id";
        var aggregateId = Guid.NewGuid();
        _aggregateReader.Setup(_ => _.LoadAsync<Pitch>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((Pitch?)null);
        var handler = new DeletePitchHandler(_eventWriterMock.Object, _aggregateReader.Object);
        var command = new DeletePitch(aggregateId);
        var wrappedCommand = new WrappedCommand<DeletePitch, Pitch>(command, ownerId);

        //Act
        var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(wrappedCommand, CancellationToken.None));

        //Assert
        Assert.Single(exception.DomainErrors);
        var domainError = exception.DomainErrors.FirstOrDefault();
        Assert.NotNull(domainError);
        Assert.Equal(Errors.AggregateNotFound.CODE, domainError.Code);
    }
}
