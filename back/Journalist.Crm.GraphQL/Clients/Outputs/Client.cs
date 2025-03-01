namespace Journalist.Crm.GraphQL.Clients.Outputs;

public record Client(Guid Id, string Name, string UserId, int NbOfPitches);
