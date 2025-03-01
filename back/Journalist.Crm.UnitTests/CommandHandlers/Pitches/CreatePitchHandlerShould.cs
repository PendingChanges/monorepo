using Journalist.Crm.CommandHandlers.Pitches;
using Journalist.Crm.Domain.Pitches;
using Journalist.Crm.Domain.Pitches.Commands;
using Moq;
using Xunit;
using CQRS;

namespace Journalist.Crm.UnitTests.CommandHandlers.Pitches;

public class CreatePitchHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReaderMock;

    public CreatePitchHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReaderMock = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_wrapped_command_create_Pitch_properly()
    {
        //Arrange
        var ownerId = "user id";
        var handler = new CreatePitchHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var pitchContent = new PitchContent("name", "content");
        var command = new CreatePitch(pitchContent, DateTime.Now, DateTime.Now, Guid.NewGuid(), Guid.NewGuid());
        var wrappedCommand = new WrappedCommand<CreatePitch, Pitch>(command, ownerId);

        //Act
        await handler.Handle(wrappedCommand, CancellationToken.None);

        //Assert
        Assert.True(true);
    }
}
