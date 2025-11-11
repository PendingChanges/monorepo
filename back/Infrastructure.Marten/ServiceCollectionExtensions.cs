using Marten;
using Marten.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CQRS;
using JasperFx.Events;

namespace Infrastructure.Marten;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomMarten(this IServiceCollection services, IConfigurationRoot configuration, Action<StoreOptions> configure)
    {
        void value(StoreOptions options)
        {
            options.Connection(configuration.GetConnectionString("Marten") ?? throw new ArgumentException("missing connection string"));

            // Events
            options.Events.StreamIdentity = StreamIdentity.AsGuid;

            configure(options);
        }

        services.AddMarten(value);

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

        return services;
    }
}
