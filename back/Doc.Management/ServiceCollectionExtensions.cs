using Doc.Management.Documents.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Management;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(CreateDocumentHandler).Assembly);
        });

        return services;
    }
}
