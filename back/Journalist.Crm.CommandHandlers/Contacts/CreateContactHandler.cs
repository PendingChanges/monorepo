using CQRS;
using Journalist.Crm.Domain.Contacts;
using Journalist.Crm.Domain.Contacts.Commands;

namespace Journalist.Crm.CommandHandlers.Contacts;

internal class CreateContactHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : SingleAggregateCommandHandler<CreateContact, Contact>(eventWriter, aggregateReader)
{
    protected override AggregateResult ExecuteCommand(Contact aggregate, CreateContact command, string ownerId) => aggregate.Create(command.Name, ownerId);

    protected override Task<Contact?> LoadAggregate(CreateContact command, string ownerId, CancellationToken cancellationToken)
        => Task.FromResult<Contact?>(new Contact());
}
