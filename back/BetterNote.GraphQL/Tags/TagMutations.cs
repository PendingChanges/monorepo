using BetterNote.Application.Tags.Commands;
using BetterNote.Infrastructure.GraphQL.Tags.Inputs;
using BetterNote.Infrastructure.GraphQL.Tags.Payloads;
using CQRS;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace BetterNote.Infrastructure.GraphQL.Tags;

[ExtendObjectType("Mutation")]
public sealed class TagMutations
{
    [GraphQLName("createTag")]
    public async Task<TagCreatedPayload> CreateTagAsync(
        [Service] ISender sender, 
        [Service] IContext context, 
        CreateTagInput input)
    {
        var createTag = new WrappedCommand<CreateTag, Domain.Tags.Tag>(new CreateTag(input.Value), context.UserId);
        var aggregate = await sender.Send(createTag);

        return new TagCreatedPayload(aggregate.Id);
    }

    [GraphQLName("deleteTag")]
    public async Task<Guid> DeleteTagAsync(
        [Service] ISender sender,
        [Service] IContext context,
        Guid id)
    {
        var deleteTag = new WrappedCommand<DeleteTag, Domain.Tags.Tag>(new DeleteTag(id), context.UserId);
        var result = await sender.Send(deleteTag);
        return result.Id;
    }
}
