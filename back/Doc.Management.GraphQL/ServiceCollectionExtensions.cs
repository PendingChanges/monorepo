using Doc.Management.GraphQL.Documents;
using Infractructure.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Management.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocManagementGraphQL(this IServiceCollection services)
    {
        services.AddCustomGraphQL(builder =>
        {
            builder
                .AddQueryType(q => q.Name("Query"))
                    .AddType<DocumentsQueries>();
        });

        return services;
    }
}
