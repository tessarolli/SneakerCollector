// <copyright file="AuthenticationResult.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Domain.Users;

namespace AuthService.Application.Authentication.Results;

/// <summary>
/// Contract for an Authentication Result response.
/// </summary>
/// <param name="User"></param>
/// <param name="Token"></param>
public record AuthenticationResult(
    User User,
    string Token);
