using CQRS;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;

namespace Journalist.Crm.CommandHandlers.Clients;

internal class DeleteClientHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<DeleteClient, Client>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Client aggregate, DeleteClient command, string ownerId)
        => aggregate.Delete(ownerId);

    protected override Task<Client?> LoadAggregate(DeleteClient command, string ownerId, CancellationToken cancellationToken)
        => AggregateReader.LoadAsync<Client>(command.Id, cancellationToken: cancellationToken);
}
