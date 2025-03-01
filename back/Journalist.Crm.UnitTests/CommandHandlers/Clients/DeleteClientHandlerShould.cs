﻿using CQRS;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.CommandHandlers.Clients;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Moq;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Clients;

public class DeleteClientHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReaderMock;

    public DeleteClientHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReaderMock = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_wrapped_command_delete_client_properly()
    {
        //Arrange
        var ownerId = "user id";
        var aggregate = new Client();
        aggregate.Create("name", ownerId);
        _aggregateReaderMock.Setup(_ => _.LoadAsync<Client>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
        var handler = new DeleteClientHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new DeleteClient(aggregate.Id);
        var wrappedCommand = new WrappedCommand<DeleteClient, Client>(command, ownerId);

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
        _aggregateReaderMock.Setup(_ => _.LoadAsync<Client>(It.IsAny<Guid>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((Client?)null);
        var handler = new DeleteClientHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new DeleteClient(aggregateId);
        var wrappedCommand = new WrappedCommand<DeleteClient, Client>(command, ownerId);

        //Act
        var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(wrappedCommand, CancellationToken.None));

        //Assert
        Assert.Single(exception.DomainErrors);
        var domainError = exception.DomainErrors.FirstOrDefault();
        Assert.NotNull(domainError);
        Assert.Equal(Errors.AggregateNotFound.CODE, domainError.Code);
    }
}
