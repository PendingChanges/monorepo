namespace Doc.Management.Documents.Events;

public sealed record DocumentCreated(
    Guid Id,
    string Key,
    string Name,
    string FileNameWithoutExtension,
    string Extension,
    string UserId,
    Version Version
);
