using Cine.Together.GraphQL.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace Cine.Together.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCineTogetherGraphQL(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
                        .AddType<MoviesQueries>()
            .AddMutationType(m => m.Name("Mutation"))
                        .AddType<MoviesMutations>();

        services.AddErrorFilter<GraphQLErrorFilter>();

        return services;
    }
}
