namespace Journalist.Crm.GraphQL.Clients.Outputs;

public sealed record Client(Guid Id, string Name, string UserId, int NbOfPitches);
