using Cine.Together.GraphQL.Movies;
using Infractructure.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Cine.Together.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCineTogetherGraphQL(this IServiceCollection services)
    {
        services.AddCustomGraphQL(builder =>
        {
            builder
                .AddQueryType(q => q.Name("Query"))
                    .AddType<MoviesQueries>()
                .AddMutationType(m => m.Name("Mutation"))
                    .AddType<MoviesMutations>();
        });

        return services;
    }
}
