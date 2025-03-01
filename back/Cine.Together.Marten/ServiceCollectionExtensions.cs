using Cine.Together.Domain.Movies;
using Cine.Together.Domain.Movies.DataModels;
using Cine.Together.Marten.Movies;
using CQRS;
using Marten;
using Marten.Events;
using Marten.Events.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cine.Together.Marten;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCineTogetherMarten(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("Marten") ?? throw new ArgumentException("missing connection string"));

            // Events
            options.Events.StreamIdentity = StreamIdentity.AsString;

            // Projections
            options.Projections.Add(new MovieProjection(), ProjectionLifecycle.Inline);

            // Indexes
            options.Schema.For<MovieDocument>().UniqueIndex(c => c.Id);
            options.Schema.For<MovieDocument>().FullTextIndex(c => c.Name);
        });

        var querySessionDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IQuerySession));

        if (querySessionDescriptor != null)
        {
            services.Remove(querySessionDescriptor);
        }
        services.AddTransient(s => s.GetRequiredService<ISessionFactory>().QuerySession());

        var documentSessionDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IDocumentSession));

        if (documentSessionDescriptor != null)
        {
            services.Remove(documentSessionDescriptor);
        }
        services.AddTransient(s => s.GetRequiredService<ISessionFactory>().OpenSession());

        services.AddTransient<AggregateRepository>();
        services.AddTransient<IWriteEvents>(sp => sp.GetRequiredService<AggregateRepository>());
        services.AddTransient<IReadAggregates>(sp => sp.GetRequiredService<AggregateRepository>());
        services.AddTransient<IReadMovies, MovieRepository>();

        return services;
    }
}
