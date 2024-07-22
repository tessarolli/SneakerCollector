// <copyright file="DependencyInjection.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Authentication;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Infrastructure.Authentication;
using AuthService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SharedDefinitions.Domain.Common.Abstractions;
using SharedDefinitions.Infrastructure.Services;

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
    public static IServiceCollection AddAuthenticationInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHashingService, PasswordHashingService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}