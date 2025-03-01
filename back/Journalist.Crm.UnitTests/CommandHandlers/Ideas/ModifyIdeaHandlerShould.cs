using CQRS;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Ideas;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.Domain.Ideas.Commands;
using Moq;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Ideas;

public class ModifyIdeaHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReaderMock;

    public ModifyIdeaHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReaderMock = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_wrapped_command_modify_idea_properly()
    {
        //Arrange
        var ownerId = "user id";
        var aggregate = new Idea();
        aggregate.Create("name", "description", ownerId);
        _aggregateReaderMock.Setup(_ => _.LoadAsync<Idea>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
        var handler = new ModifyIdeaHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new ModifyIdea(aggregate.Id, "new name", "new description");
        var wrappedCommand = new WrappedCommand<ModifyIdea, Idea>(command, ownerId);

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
        _aggregateReaderMock.Setup(_ => _.LoadAsync<Idea>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((Idea?)null);
        var handler = new ModifyIdeaHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new ModifyIdea(aggregateId, "new name", "new description");
        var wrappedCommand = new WrappedCommand<ModifyIdea, Idea>(command, ownerId);

        //Act
        var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(wrappedCommand, CancellationToken.None));

        //Assert
        Assert.Single(exception.DomainErrors);
        var domainError = exception.DomainErrors.FirstOrDefault();
        Assert.NotNull(domainError);
        Assert.Equal(Errors.AggregateNotFound.CODE, domainError.Code);
    }
}
