namespace CQRS;

public class DomainException(IEnumerable<Error>? errors) : Exception
{
    public IEnumerable<Error> DomainErrors { get; } = errors ?? [];
}
