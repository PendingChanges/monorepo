namespace BetterNote.Domain.Subjects.Events;
public record SubjectCreated(Guid Id, string Title, string Description, IReadOnlyCollection<Guid> TagsId);
