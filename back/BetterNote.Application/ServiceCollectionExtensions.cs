using BetterNote.Application.Tags.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace BetterNote.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(CreateTagHandler).Assembly);
        });

        return services;
    }
}
