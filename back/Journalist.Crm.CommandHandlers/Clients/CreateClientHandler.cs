using CQRS;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;

namespace Journalist.Crm.CommandHandlers.Clients;

internal class CreateClientHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<CreateClient, Client>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Client aggregate, CreateClient command, string ownerId) => aggregate.Create(command.Name, ownerId);

    protected override Task<Client?> LoadAggregate(CreateClient command, string ownerId,
        CancellationToken cancellationToken) => Task.FromResult<Client?>(new Client());
}
