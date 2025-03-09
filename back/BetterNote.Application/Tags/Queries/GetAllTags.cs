using MediatR;

namespace BetterNote.Application.Tags.Queries;
public record GetAllTags() : IRequest<IReadOnlyList<TagDocument>>;
