using Journalist.Crm.Domain.Clients.DataModels;
using Journalist.Crm.GraphQL.Clients.Inputs;
using Journalist.Crm.GraphQL.Clients.Outputs;

namespace Journalist.Crm.GraphQL.Clients;

public static class ClientMapper
{
    public static Client? ToClientOrNull(this ClientDocument? clientDocument)
        => clientDocument?.ToClient();

    public static Client ToClient(this ClientDocument clientDocument)
        => new(clientDocument.Id, clientDocument.Name, clientDocument.OwnerId, clientDocument.PitchesIds.Count);

    public static IReadOnlyList<Client> ToClients(this IReadOnlyList<ClientDocument> clients)
        => clients.Select(ToClient).ToList();

    public static Domain.Clients.Commands.RenameClient ToCommand(this RenameClient renameClient)
        => new(renameClient.Id, renameClient.NewName);

    public static Domain.Clients.Commands.DeleteClient ToCommand(this DeleteClient deleteClient)
        => new(deleteClient.Id);

    public static Domain.Clients.Commands.CreateClient ToCommand(this CreateClient createClient)
        => new(createClient.Name);
}
