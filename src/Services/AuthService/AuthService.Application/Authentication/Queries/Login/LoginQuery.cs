// <copyright file="LoginQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Authentication.Results;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Authentication.Queries.Login;

/// <summary>
/// Contract for the Login Query.
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record LoginQuery(
    string Email,
    string Password) : IQuery<AuthenticationResult>;
