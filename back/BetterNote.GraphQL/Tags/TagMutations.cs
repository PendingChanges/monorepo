using BetterNote.Tags.Commands;
using CQRS;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace BetterNote.Infrastructure.GraphQL.Tags;

[ExtendObjectType("Mutation")]
public class TagMutations
{
    [GraphQLName("createTag")]
    public async Task<Guid> CreateTagAsync(
        [Service] ISender sender, 
        [Service] IContext context, 
        string value)
    {
        var createTag = new WrappedCommand<CreateTag, Domain.Tags.Tag>(new CreateTag(value), context.UserId);
        var aggregate = await sender.Send(createTag);

        return aggregate.Id;
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
