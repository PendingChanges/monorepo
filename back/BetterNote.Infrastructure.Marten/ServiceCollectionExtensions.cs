using BetterNote.Application.Subjects;
using BetterNote.Application.Tags;
using BetterNote.Infrastructure.Marten.Subjects;
using BetterNote.Infrastructure.Marten.Tags;
using Infrastructure.Marten;
using JasperFx.Events.Projections;
using Marten.Events.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BetterNote.Infrastructure.Marten;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBetterNoteMarten(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddCustomMarten(configuration, options =>
        {
            // Projections
            options.Projections.Add(new TagProjection(), ProjectionLifecycle.Inline);
            options.Projections.Add(new SubjectProjection(), ProjectionLifecycle.Inline);

            // Indexes
            options.Schema.For<TagDocument>().FullTextIndex(c => c.Value);
            options.Schema.For<SubjectDocument>().FullTextIndex(c => c.Title);
            options.Schema.For<SubjectDocument>().FullTextIndex(c => c.Description);
        });

        services.AddTransient<IReadTags, TagRepository>();
        services.AddTransient<IReadSubjects, SubjectRepository>();

        return services;
    }
}
