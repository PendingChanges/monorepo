using Cine.Together.Domain.Movies;
using Cine.Together.Domain.Movies.DataModels;
using Cine.Together.Marten.Movies;
using Infrastructure.Marten;
using JasperFx.Events.Projections;
using Marten.Events.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cine.Together.Marten;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCineTogetherMarten(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddCustomMarten(configuration, options =>
        {
            // Projections
            options.Projections.Add(new MovieProjection(), ProjectionLifecycle.Inline);

            // Indexes
            options.Schema.For<MovieDocument>().UniqueIndex(c => c.Id);
            options.Schema.For<MovieDocument>().FullTextIndex(c => c.Name);

        });

        services.AddTransient<IReadMovies, MovieRepository>();

        return services;
    }
}
