﻿using MediatR;

namespace BetterNote.Application.Tags.Queries.Handlers;
public class GetAllTagsHandler(IReadTags tagReader) : IRequestHandler<GetAllTags, IReadOnlyList<TagDocument>>
{
    public Task<IReadOnlyList<TagDocument>> Handle(GetAllTags request, CancellationToken cancellationToken) => tagReader.GetAllTagAsync(cancellationToken);
}
