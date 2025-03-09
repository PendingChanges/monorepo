using CQRS;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.GraphQL.Clients.Inputs;
using Journalist.Crm.GraphQL.Clients.Outputs;
using MediatR;

namespace Journalist.Crm.GraphQL.Clients;

[ExtendObjectType("Mutation")]
public class ClientsMutations
{
    [Authorize(Roles = ["user"])]
    [GraphQLName("addClient")]
    public async Task<ClientAddedPayload> AddClientAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        CreateClient createClient,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Clients.Commands.CreateClient, Domain.Clients.Client>(createClient.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new ClientAddedPayload { ClientId = result.Id };
    }

    [Authorize(Roles = ["user"])]
    [GraphQLName("removeClient")]
    public async Task<Guid> RemoveClientAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        DeleteClient deleteClient,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Clients.Commands.DeleteClient, Domain.Clients.Client>(deleteClient.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }

    [Authorize(Roles = ["user"])]
    [GraphQLName("renameClient")]
    public async Task<Guid> RenameClientAsync([Service] IMediator mediator, [Service] IContext context, RenameClient renameClient,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Clients.Commands.RenameClient, Domain.Clients.Client>(renameClient.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }
}
