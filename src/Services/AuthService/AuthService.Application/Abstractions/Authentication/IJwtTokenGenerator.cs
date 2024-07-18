// <copyright file="IJwtTokenGenerator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Domain.Users;

namespace AuthService.Application.Abstractions.Authentication;

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
