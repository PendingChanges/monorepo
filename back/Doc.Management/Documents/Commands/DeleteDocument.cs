using CQRS;

namespace Doc.Management.Documents.Commands;

public sealed record DeleteDocument(Guid Id) : ICommand;
