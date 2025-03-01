﻿using Doc.Management.Documents;
using Doc.Management.Documents.DataModels;
using Marten;
using Marten.Pagination;

namespace Doc.Management.Marten.Documents;

public class DocumentRepository(IQuerySession session) : IReadDocuments
{
    private readonly IQuerySession _session = session;

    public Task<DocumentDocument?> GetDocumentByIdAsync(
        Guid id,
        Version? version,
        CancellationToken cancellationToken = default
    )
    {
        var query = _session.Query<DocumentDocument>().Where(d => d.Id == id);

        if (version != null)
        {
            query = query.Where(d => d.Version == version);
        }

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DocumentResultSet> GetDocumentsAsync(
        GetDocumentsRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = _session.Query<DocumentDocument>().Where(d => true);

        query = SortBy(request, query);

        var pageNumber = request.Skip / request.Take + 1;

        var pagedResult = await query.ToPagedListAsync(pageNumber, request.Take, cancellationToken);

        return new DocumentResultSet(
            [.. pagedResult],
            (int)pagedResult.TotalItemCount,
            pagedResult.HasNextPage,
            pagedResult.HasPreviousPage
        );
    }

    private static IQueryable<DocumentDocument> SortBy(
        GetDocumentsRequest request,
        IQueryable<DocumentDocument> query
    ) =>
        request.SortDirection switch
        {
            "desc"
                => request.SortBy switch
                {
                    _ => query.OrderByDescending(c => c.Name)
                },
            _
                => request.SortBy switch
                {
                    _ => query.OrderBy(c => c.Name)
                },
        };
}
