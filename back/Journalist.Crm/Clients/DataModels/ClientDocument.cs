namespace Journalist.Crm.Domain.Clients.DataModels;

public sealed record ClientDocument(Guid Id, string Name, string OwnerId, List<Guid> PitchesIds);
