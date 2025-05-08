namespace BetterNote.Domain.Persons.Events;
public sealed record PersonCreated(Guid PersonId, string FirstName, string LastName);
