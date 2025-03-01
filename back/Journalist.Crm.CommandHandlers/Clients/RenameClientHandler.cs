using CQRS;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Clients.Commands;

namespace Journalist.Crm.CommandHandlers.Clients;

internal class RenameClientHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<RenameClient, Client>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Client aggregate, RenameClient command, string ownerId)
        => aggregate.Rename(command.NewName, ownerId);


    protected override Task<Client?> LoadAggregate(RenameClient command, string ownerId, CancellationToken cancellationToken)
        => AggregateReader.LoadAsync<Client>(command.Id, cancellationToken: cancellationToken);
}
