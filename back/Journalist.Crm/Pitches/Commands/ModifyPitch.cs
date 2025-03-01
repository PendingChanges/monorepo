﻿using CQRS;

namespace Journalist.Crm.Domain.Pitches.Commands;

public record ModifyPitch(Guid Id, PitchContent Content, DateTime? DeadLineDate, DateTime? IssueDate, Guid ClientId, Guid IdeaId) : ICommand;
