namespace BetterNote.Domain.Persons.Events;
public record PersonCreated(Guid Id, string FirstName, string LastName);
