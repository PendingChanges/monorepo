using CQRS;
using Infrastructure.Marten;
using Marten;
using Marten.Events;
using Moq;
using System.Data;
using Xunit;

namespace Tests.Common.Marten;

public sealed class AggregateRepositoryShould
{
    private readonly Mock<IDocumentStore> _mockDocumentStore;
    private readonly Mock<IDocumentSession> _mockSession;
    private readonly AggregateRepository _repository;

    public AggregateRepositoryShould()
    {
        _mockDocumentStore = new Mock<IDocumentStore>();
        _mockSession = new Mock<IDocumentSession>();
        var eventStoreMock = new Mock<IEventStoreOperations>();
        _mockSession.Setup(_ => _.Events).Returns(eventStoreMock.Object);
        _mockDocumentStore
            .Setup(x => x.LightweightSession(It.IsAny<IsolationLevel>()))
            .Returns(_mockSession.Object);
        _repository = new AggregateRepository(_mockDocumentStore.Object);
    }

    [Fact]
    public async Task StoreEventsCorrectly()
    {
        // Arrange
        var aggregateId = Guid.NewGuid();
        const long version = 1L;
        var events = new List<object> { new { Name = "TestEvent" } };
        var cancellationToken = new CancellationToken();

        // Act
        await _repository.StoreAsync(aggregateId, version, events, cancellationToken);

        // Assert
        _mockSession.Verify(x => x.Events.Append(aggregateId, version, events), Times.Once);
        _mockSession.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task LoadAggregateCorrectly()
    {
        // Arrange
        var aggregateId = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;
        var expectedAggregate = new Mock<Aggregate>().Object;

        _mockSession
            .Setup(x =>
                x.Events.AggregateStreamAsync(
                    aggregateId,
                    It.IsAny<long>(),
                    It.IsAny<DateTimeOffset?>(),
                    It.IsAny<Aggregate>(),
                    It.IsAny<long>(),
                    cancellationToken
                )
            )
            .ReturnsAsync(expectedAggregate);

        // Act
        var result = await _repository.LoadAsync<Aggregate>(
            aggregateId,
            cancellationToken: cancellationToken
        );

        // Assert
        Assert.Equal(expectedAggregate, result);
    }
}
