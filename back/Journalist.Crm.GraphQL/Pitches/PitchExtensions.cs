using HotChocolate.Authorization;
using Journalist.Crm.Domain.Clients;
using Journalist.Crm.Domain.Ideas;
using Journalist.Crm.GraphQL.Clients;
using Journalist.Crm.GraphQL.Ideas;
using Journalist.Crm.GraphQL.Pitches.Outputs;
using Client = Journalist.Crm.GraphQL.Clients.Outputs.Client;
using CQRS;
using HotChocolate;
using HotChocolate.Types;

namespace Journalist.Crm.GraphQL.Pitches;


//TODO: Create dataloaders for all extensions
[ExtendObjectType(typeof(Pitch))]
public class PitchExtensions
{
    [Authorize(Roles = ["user"])]
    public async Task<Ideas.Outputs.Idea?> GetIdeaAsync(
[Parent] Pitch pitch,
[Service] IReadIdeas ideasReader,
[Service] IContext context,
CancellationToken cancellationToken = default)
    => (await ideasReader.GetIdeaAsync(pitch.IdeaId, context.UserId, cancellationToken)).ToIdeaOrNull();

    [Authorize(Roles = ["user"])]
    public async Task<Client?> GetClientAsync(
[Parent] Pitch pitch,
[Service] IReadClients clientsReader,
[Service] IContext context,
CancellationToken cancellationToken = default)
    => (await clientsReader.GetClientAsync(pitch.ClientId, context.UserId, cancellationToken)).ToClientOrNull();

    [Authorize(Roles = ["user"])]
    public async Task<PitchGuards?> GetGuardsAsync(
        [Parent] Pitch pitch,
        [Service] IReadAggregates aggregateReader,
        [Service] IContext context,
        CancellationToken cancellationToken = default)
    => (await aggregateReader.LoadAsync<Domain.Pitches.Pitch>(pitch.Id, cancellationToken: cancellationToken)).ToPitchGuardsOrNull();
}
