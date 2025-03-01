namespace CQRS;
public interface IWriteEvents
{
    Task StoreAsync(Guid aggregateId, long version, IEnumerable<object> events, CancellationToken ct = default);
}