using CQRS;
using HotChocolate.Authorization;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.Domain;
using Journalist.Crm.GraphQL.Ideas.Inputs;
using Journalist.Crm.GraphQL.Ideas.Outputs;
using MediatR;
using Idea = Journalist.Crm.Domain.Ideas.Idea;

namespace Journalist.Crm.GraphQL.Ideas;

[ExtendObjectType("Mutation")]
public class IdeasMutations
{
    [Authorize(Roles = ["user"])]
    public async Task<IdeaAddedPayload> AddIdeaAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
    CreateIdea createIdea,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Ideas.Commands.CreateIdea, Idea>(createIdea.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new IdeaAddedPayload { IdeaId = result.Id };
    }

    [Authorize(Roles = ["user"])]
    public async Task<Guid> RemoveIdeaAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        DeleteIdea deleteIdea,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Ideas.Commands.DeleteIdea, Idea>(deleteIdea.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }

    [Authorize(Roles = ["user"])]
    [GraphQLName("modifyIdea")]
    public async Task<Guid> ModifyIdeaAsync([Service] IMediator mediator, [Service] IContext context, ModifyIdea modifyIdea,
    CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Ideas.Commands.ModifyIdea, Idea>(modifyIdea.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }
}
