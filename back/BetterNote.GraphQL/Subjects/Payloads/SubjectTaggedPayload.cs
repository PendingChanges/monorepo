namespace BetterNote.Infrastructure.GraphQL.Subjects.Payloads;
public sealed record SubjectTaggedPayload(Guid SubjectId, Guid TagId);