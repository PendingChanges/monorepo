namespace CQRS;

public class DomainErrorCollection
{
    private readonly List<DomainError> _errors = [];

    public bool HasErrors => _errors.Count > 0;

    public void Add(DomainError error) => _errors.Add(error);

    public void AddError(DomainError error) => Add(error);

    public IEnumerable<DomainError> GetErrors() => _errors;
}
