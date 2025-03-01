using CQRS;

namespace Journalist.Crm.Domain.Pitches.Commands;

public record DeletePitch(Guid Id) : ICommand;
