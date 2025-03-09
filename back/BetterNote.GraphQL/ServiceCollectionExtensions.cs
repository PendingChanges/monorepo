using BetterNote.Infrastructure.GraphQL.Subjects;
using BetterNote.Infrastructure.GraphQL.Tags;
using Infractructure.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Cine.Together.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBetterNoteGraphQL(this IServiceCollection services)
    {
        services.AddCustomGraphQL(builder =>
        {
            builder
                .AddQueryType(q => q.Name("Query"))
                    .AddType<TagQueries>()
                    .AddType<SubjectQueries>()
                .AddMutationType(m => m.Name("Mutation"))
                    .AddType<TagMutations>()
                    .AddType<SubjectMutations>()
                    ;
        });

        return services;
    }
}
