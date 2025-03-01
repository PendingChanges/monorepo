namespace CQRS;

public interface IReadAggregates
{
    Task<T?> LoadAsync<T>(Guid id, int? version = null, CancellationToken cancellationToken = default) where T : Aggregate;
}