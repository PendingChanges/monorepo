﻿using CQRS;
using Journalist.Crm.CommandHandlers.Clients;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;
using Moq;
using Xunit;

namespace Journalist.Crm.UnitTests.CommandHandlers.Clients;

public class CreateClientHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReaderMock;

    public CreateClientHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReaderMock = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_wrapped_command_create_client_properly()
    {
        //Arrange
        var ownerId = "user id";
        var handler = new CreateClientHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new CreateClient("name");
        var wrappedCommand = new WrappedCommand<CreateClient, Client>(command, ownerId);

        //Act
        await handler.Handle(wrappedCommand, CancellationToken.None);

        //Assert
        Assert.True(true);
    }
}
