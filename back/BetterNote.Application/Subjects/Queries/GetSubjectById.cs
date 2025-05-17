using MediatR;

namespace BetterNote.Application.Subjects.Queries;
public sealed record GetSubjectById(Guid Id) : IRequest<SubjectDocument>;
