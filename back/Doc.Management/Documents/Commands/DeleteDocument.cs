using CQRS;

namespace Doc.Management.Documents.Commands;

public record DeleteDocument(Guid Id) : ICommand;
