using CQRS;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Journalist.Crm.GraphQL.Contacts.Inputs;
using Journalist.Crm.GraphQL.Contacts.Outputs;
using MediatR;

namespace Journalist.Crm.GraphQL.Contacts;

[ExtendObjectType("Mutation")]
public class ContactMutations
{
    [Authorize(Roles = ["user"])]
    [GraphQLName("addContact")]
    public async Task<ContactAddedPayload> AddContactAsync(
    [Service] IMediator mediator,
    [Service] IContext context,
    AddContact addContact,
    CancellationToken cancellationToken = default)
    {
        var command = new WrappedCommand<Domain.Contacts.Commands.CreateContact, Domain.Contacts.Contact>(addContact.ToCommand(), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        return new ContactAddedPayload { ContactId = result.Id };
    }
}
