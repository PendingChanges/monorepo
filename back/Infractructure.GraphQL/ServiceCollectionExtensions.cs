using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infractructure.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomGraphQL(this IServiceCollection services, Action<IRequestExecutorBuilder> configure)
    {
        var requestExecutorBuilder = services.AddGraphQLServer()
             .AddAuthorization();

        configure(requestExecutorBuilder);

        services.AddErrorFilter<GraphQLErrorFilter>();

        return services;
    }
}
