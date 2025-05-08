namespace CQRS;

public sealed record DomainError(string Code, string Label);
