namespace BetterNote.Domain.Persons.Events;
public record PersonCreated(Guid PersonId, string FirstName, string LastName);
