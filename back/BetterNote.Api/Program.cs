using CQRS;
using BetterNote.Application;
using BetterNote.Infrastructure.Marten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Compact;
using System.Text.Json.Serialization;
using Cine.Together.Api.Infrastructure;
using Infrastructure.Api;
using BetterNote.Infrastructure.GraphQL;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    var MyAllowSpecificOrigins = "LocalOnly";

    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var authenticationOptions = configuration
        .GetSection(KeycloakAuthenticationOptions.Section)
        .Get<KeycloakAuthenticationOptions>();

    builder.Host.UseSerilog();

    builder
        .Services.AddCommandHandlers()
        .AddBetterNoteMarten(configuration)
        .AddBetterNoteGraphQL()
        .AddHealthChecks();

    builder.Services.AddHttpContextAccessor().AddTransient<IContext, Context>();

    builder.Services.AddAntiforgery();

    builder.Services.AddKeycloackAuthentication(authenticationOptions!);

    builder.Services.AddAuthorization(
        (options) =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.AddPolicy("Administrators", policy => policy.RequireClaim("roles", "[admin]"));
            options.AddPolicy("Users", policy => policy.RequireClaim("roles", "[user]"));
        }
    );

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            MyAllowSpecificOrigins,
            policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }
        );
    });

    var app = builder.Build();
    app.UseAntiforgery();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors(MyAllowSpecificOrigins);
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapGraphQL();

    app.MapWhen(
        context =>
            !(context.Request.Path.Value ?? string.Empty).Contains("/graphql")
            && !(context.Request.Path.Value ?? string.Empty).Contains("/healthz")
            && !(context.Request.Path.Value ?? string.Empty).Contains("/api"),
        app =>
        {
            app.Use(
                    (context, next) =>
                    {
                        context.Request.Path = "/index.html";
                        return next();
                    }
                )
                .UseStaticFiles();
        }
    );

    app.MapHealthChecks("/healthz");

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
