namespace CQRS;

public class DomainException(IEnumerable<DomainError>? errors) : Exception
{
    public IEnumerable<DomainError> DomainErrors { get; } = errors ?? [];
}
