namespace Journalist.Crm.Domain.Clients.DataModels;

public record ClientDocument(Guid Id, string Name, string OwnerId, List<Guid> PitchesIds);
