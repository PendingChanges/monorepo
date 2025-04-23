namespace BetterNote.Domain.Subjects.Events;

//TODO : virer la collection de tags et passer en events TagAddedToSubject  
public record SubjectCreated(Guid Id, string Title, string Description, IReadOnlyCollection<Guid> TagsId);
