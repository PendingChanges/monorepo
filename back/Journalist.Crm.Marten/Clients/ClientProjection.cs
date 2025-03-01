﻿using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.Domain.Clients.Events;
using Marten;
using Marten.Events.Projections;

namespace Journalist.Crm.Marten.Clients;

public class ClientProjection : EventProjection
{
    public ClientDocument Create(ClientCreated clientCreated)
        => new(clientCreated.Id, clientCreated.Name, clientCreated.OwnerId, []);

    public void Project(ClientDeleted @event, IDocumentOperations ops)
        => ops.Delete<ClientDocument>(@event.Id);

    public async Task Project(ClientRenamed @event, IDocumentOperations ops)
    {
        var client = await ops.Query<ClientDocument>().SingleOrDefaultAsync(c => c.Id == @event.Id);

        if (client != null)
        {
            var clientUpdated = client with { Name = @event.NewName };

            ops.Store(clientUpdated);
        }
    }
}
