namespace Doc.Management.GraphQL.Documents.Outputs;

public sealed record Document(
    Guid Id,
    string Name,
    string FileNameWithoutExtension,
    string Extension,
    Version Version
);
