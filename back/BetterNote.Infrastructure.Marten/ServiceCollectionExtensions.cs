using BetterNote.Application.Tags;
using BetterNote.Infrastructure.Marten.Tags;
using Infrastructure.Marten;
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

            // Indexes
            options.Schema.For<TagDocument>().FullTextIndex(c => c.Value);
        });

        services.AddTransient<IReadTags, TagRepository>();

        return services;
    }
}
