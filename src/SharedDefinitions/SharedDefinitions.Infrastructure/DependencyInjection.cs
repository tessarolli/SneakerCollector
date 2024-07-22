// <copyright file="DependencyInjection.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Text;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Infrastructure.Abstractions;
using SharedDefinitions.Infrastructure.Authentication;
using SharedDefinitions.Infrastructure.Services;
using SharedDefinitions.Infrastructure.Utilities;

namespace SharedDefinitions.Infrastructure;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Infrastructure dependencies.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder being extended.</param>
    /// <param name="serviceName">The name of this running service.</param>
    /// <returns>Services with dependencies injected.</returns>
    public static IServiceCollection AddSharedDefinitionsInfrastructure(this WebApplicationBuilder builder, string serviceName = "")
    {
        builder
            .AddLoggingAndTracing(serviceName);

        builder.Services
             .AddSharedDefinitionsPersistance()
             .AddSharedDefinitionsAuthentication();

        builder.Services.AddDaprClient();

        builder.Services.AddScoped<IExceptionHandlingService, ExceptionHandlingService>();

        builder.Services.AddScoped<ISqlBuilderService, SqlBuilderService>();

        return builder.Services;
    }

    private static void AddLoggingAndTracing(this WebApplicationBuilder builder, string serviceName)
    {
        builder.Services.AddApplicationInsightsTelemetry();

        builder.Host.UseSerilog((chostBuilderContextontext, services, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", serviceName)
                .WriteTo.Async(a => a.Console(outputTemplate: "{Application}: [{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", theme: AnsiConsoleTheme.Code));

            // Adding Seq sink
            loggerConfiguration.WriteTo.Seq("http://seq");

            // Adding File sink
            loggerConfiguration.WriteTo.File("logs/access-log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null);

            // Adding Application Insights sink
            loggerConfiguration.WriteTo.ApplicationInsights(services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces);
        });
    }

    /// <summary>
    /// Add Persistence dependencies.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <returns>Services with dependencies injected.</returns>
    private static IServiceCollection AddSharedDefinitionsPersistance(this IServiceCollection services)
    {
        services.AddScoped<IDapperUtility, DapperUtility>();

        services.AddScoped<ISqlConnectionFactory, PostgresSqlConnectionFactory>();

        return services;
    }

    /// <summary>
    /// Add Authentication dependencies.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <returns>Services with dependencies injected.</returns>
    private static IServiceCollection AddSharedDefinitionsAuthentication(this IServiceCollection services)
    {
        var jwtSettings = new JwtSettings
        {
            Secret = "my-super-secret-key@!12345678901",
            ExpireDays = 1,
            Issuer = "SneakerCollector",
            Audience = "SneakerCollector",
        };

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });

        return services;
    }
}
