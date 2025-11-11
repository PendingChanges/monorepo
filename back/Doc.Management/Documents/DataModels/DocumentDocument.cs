namespace Doc.Management.Documents.DataModels;

public sealed record DocumentDocument(
    Guid Id,
    string Key,
    string Name,
    string FileNameWithoutExtension,
    string Extension,
    Version Version
);
