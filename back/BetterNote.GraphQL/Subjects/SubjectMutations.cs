using BetterNote.Application.Subjects.Commands;
using BetterNote.Domain.Subjects;
using BetterNote.Infrastructure.GraphQL.Subjects.Inputs;
using BetterNote.Infrastructure.GraphQL.Subjects.Payloads;
using CQRS;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace BetterNote.Infrastructure.GraphQL.Subjects;

[ExtendObjectType("Mutation")]
public class SubjectMutations
{
    [GraphQLName("createSubject")]
    public async Task<SubjectCreatedPayload> CreateSubjectAsync(
    [Service] ISender sender,
    [Service] IContext context,
    CreateSubjectInput input
    )
    {
        var createTag = new WrappedCommand<CreateSubject, Subject>(new CreateSubject(input.Title, input.Description, input.TagsId ?? []), context.UserId);
        var aggregate = await sender.Send(createTag);

        return new SubjectCreatedPayload(aggregate.Id);
    }
}
