using Cine.Together.Domain.Movies.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Cine.Together.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(CreateMovieHandler).Assembly);
        });

        return services;
    }
}
