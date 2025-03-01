﻿using CQRS;
using Doc.Management.ValueObjects;

namespace Doc.Management.Documents.Commands;

public record CreateDocument(
    DocumentKey Key,
    string Name,
    string FileNameWithoutExtension,
    string Extension
) : ICommand;
