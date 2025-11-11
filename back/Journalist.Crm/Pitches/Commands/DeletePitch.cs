using CQRS;

namespace Journalist.Crm.Domain.Pitches.Commands;

public sealed record DeletePitch(Guid Id) : ICommand;
