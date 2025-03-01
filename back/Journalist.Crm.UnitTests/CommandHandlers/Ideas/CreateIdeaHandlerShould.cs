using CQRS;
using Journalist.Crm.CommandHandlers.Ideas;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Moq;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Ideas;

public class CreateIdeaHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReaderMock;

    public CreateIdeaHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReaderMock = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_wrapped_command_create_idea_properly()
    {
        //Arrange
        var ownerId = "user id";
        var handler = new CreateIdeaHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new CreateIdea("name", "description");
        var wrappedCommand = new WrappedCommand<CreateIdea, Idea>(command, ownerId);

        //Act
        await handler.Handle(wrappedCommand, CancellationToken.None);

        //Assert
        Assert.True(true);
    }
}
