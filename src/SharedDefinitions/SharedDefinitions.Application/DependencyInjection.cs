// <copyright file="DependencyInjection.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedDefinitions.Application;
using SharedDefinitions.Application.Common.Behaviors;

namespace SharedDefinitions.Application;

/// <summary>
/// Provides support for Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Injects all dependency for the Application Layer.
    /// </summary>
    /// <param name="services">IServiceCollection instance.</param>
    /// <param name="assembly">The current running assembly (dll) to register the mediator types from.</param>
    /// <returns>IServiceCollection with dependencies injected.</returns>
    public static IServiceCollection AddSharedDefinitionsApplication(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(assembly);

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }

    /// <summary>
    /// Injects all dependency for the MediatR.
    /// </summary>
    /// <param name="services">IServiceCollection instance.</param>
    /// <param name="assembly">Assembly.</param>
    /// <returns>IServiceCollection with dependencies injected.</returns>
    private static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineRequestValidationBehavior<,>));

        return services;
    }
}
