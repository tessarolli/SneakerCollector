// <copyright file="DependencyInjection.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Infrastructure.Repositories;

namespace ProductService.Infrastructure;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Persistence dependencies.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <returns>Services with dependencies injected.</returns>
    public static IServiceCollection AddProductServicePersistance(this IServiceCollection services)
    {
        services.AddScoped<IShoeRepository, ShoeRepository>();

        return services;
    }
}