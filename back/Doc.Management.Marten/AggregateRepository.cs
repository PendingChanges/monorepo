﻿using CQRS;
using Marten;

namespace Doc.Management.Marten;

public sealed class AggregateRepository(IDocumentStore store) : IWriteEvents, IReadAggregates
{
    private readonly IDocumentStore _store = store;

    public async Task StoreAsync(
        Guid aggregateId,
        long version,
        IEnumerable<object> events,
        CancellationToken ct = default
    )
    {
        await using var session = _store.LightweightSession();

        session.Events.Append(aggregateId, version, events);

        await session.SaveChangesAsync(ct);
    }

    public async Task<T?> LoadAsync<T>(
        Guid id,
        int? version = null,
        CancellationToken cancellationToken = default
    )
        where T : Aggregate
    {
        await using var session = _store.LightweightSession();
        var aggregate = await session.Events.AggregateStreamAsync<T>(
            id,
            version ?? 0,
            token: cancellationToken
        );
        return aggregate;
    }
}
