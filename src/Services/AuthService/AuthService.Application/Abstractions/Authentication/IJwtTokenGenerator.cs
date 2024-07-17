// <copyright file="IJwtTokenGenerator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.Services.AuthService.Domain.Users;

namespace SneakerCollector.Services.AuthService.Application.Abstractions.Authentication;

/// <summary>
/// Interface for Json Web Tokens Generator.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT token.
    /// </summary>
    /// <param name="user">The User to generate the token for.</param>
    /// <returns>The token.</returns>
    string GenerateToken(User user);
}
