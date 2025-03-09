using Doc.Management.Documents;
using Doc.Management.Documents.DataModels;
using Doc.Management.Marten.Documents;
using Infrastructure.Marten;
using Marten.Events.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Management.Marten;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocManagementMarten(
        this IServiceCollection services,
        IConfigurationRoot configuration
    )
    {
        services.AddCustomMarten(configuration, options => {
            // Projections
            options.Projections.Add(new DocumentProjection(), ProjectionLifecycle.Inline);

            // Indexes
            options.Schema.For<DocumentDocument>().UniqueIndex(c => c.Id);
            options.Schema.For<DocumentDocument>().UniqueIndex(c => c.Key);
            options.Schema.For<DocumentDocument>().FullTextIndex(c => c.FileNameWithoutExtension);

        });

        services.AddTransient<IReadDocuments, DocumentRepository>();

        return services;
    }
}
