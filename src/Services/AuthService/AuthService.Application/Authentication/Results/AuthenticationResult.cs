// <copyright file="AuthenticationResult.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.Services.AuthService.Domain.Users;

namespace SneakerCollector.Services.AuthService.Application.Authentication.Results;

/// <summary>
/// Contract for an Authentication Result response.
/// </summary>
/// <param name="User"></param>
/// <param name="Token"></param>
public record AuthenticationResult(
    User User,
    string Token);
