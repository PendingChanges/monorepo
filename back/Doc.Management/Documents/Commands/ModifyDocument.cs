using CQRS;
using Doc.Management.ValueObjects;

namespace Doc.Management.Documents.Commands;

public sealed record ModifyDocument(
    Guid DocumentId,
    DocumentKey Key,
    string Name,
    string FileNameWithoutExtension,
    string Extension,
    VersionIncrementType VersionIncrementType
) : ICommand
{ }
