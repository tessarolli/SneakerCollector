// <copyright file="DependencyInjection.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Abstractions.Services;
using ProductService.Infrastructure.Repositories;
using ProductService.Infrastructure.Services;

namespace ProductService.Infrastructure;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Product Service Infrastructure dependencies.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <returns>Services with dependencies injected.</returns>
    public static IServiceCollection AddProductServiceInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IShoeRepository, ShoeRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();

        services.AddScoped<IUserService, UserService>();

        return services;
    }
}