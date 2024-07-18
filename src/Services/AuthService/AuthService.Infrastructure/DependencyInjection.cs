// <copyright file="DependencyInjection.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Authentication;
using AuthService.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Authentication dependencies.
    /// </summary>
    /// <param name="services">Injected services.</param>
    /// <returns>Services with dependencies injected.</returns>
    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}