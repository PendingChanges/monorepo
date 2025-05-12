using BetterNote.Infrastructure.GraphQL.Subjects;
using BetterNote.Infrastructure.GraphQL.Tags;
using BetterNote.Infrastructure.Marten.Tags;
using Infractructure.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace BetterNote.Infrastructure.GraphQL;

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
                .AddTypeExtension<SubjectExtensions>()
                .AddDataLoader<TagsBySubjectIdDataLoader>()
                    ;
        });

        return services;
    }
}
