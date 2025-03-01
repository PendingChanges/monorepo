using CQRS;
using HotChocolate.Authorization;
using Journalist.Crm.CommandHandlers;
using Journalist.Crm.Domain;
using Journalist.Crm.GraphQL.Pitches.Inputs;
using Journalist.Crm.GraphQL.Pitches.Outputs;
using MediatR;

namespace Journalist.Crm.GraphQL.Pitches;

[ExtendObjectType("Mutation")]
public class PitchesMutations
{

    [GraphQLName("addPitch")]
    [Authorize(Roles = ["user"])]
    public async Task<PitchAddedPayload> AddPitchAsync(
                [Service] IMediator mediator,
    [Service] IContext context,
CreatePitch createPitch,
CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Pitches.Commands.CreatePitch, Domain.Pitches.Pitch>(createPitch.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new PitchAddedPayload { PitchId = result.Id };
    }

    [GraphQLName("removePitch")]
    [Authorize(Roles = ["user"])]
    public async Task<Guid> RemovePitchAsync(
                [Service] IMediator mediator,
    [Service] IContext context,
        DeletePitch deletePitch,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Pitches.Commands.DeletePitch, Domain.Pitches.Pitch>(deletePitch.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }

    [GraphQLName("modifyPitch")]
    [Authorize(Roles = ["user"])]
    public async Task<Guid> ModifyPitchAsync(
        [Service] IMediator mediator,
        [Service] IContext context,
        ModifyPitch modifyPitch,
        CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Pitches.Commands.ModifyPitch, Domain.Pitches.Pitch>(modifyPitch.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return result.Id;
    }
}
