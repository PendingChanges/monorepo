namespace CQRS;

public class ErrorCollection
{
    private readonly List<Error> _errors = [];

    public bool HasErrors => _errors.Count > 0;

    public void Add(Error error) => _errors.Add(error);

    public void AddError(Error error) => Add(error);

    public IEnumerable<Error> GetErrors() => _errors;
}
