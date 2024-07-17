// <copyright file="LoginQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.Services.AuthService.Application.Authentication.Results;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.AuthService.Application.Authentication.Queries.Login;

/// <summary>
/// Contract for the Login Query.
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record LoginQuery(
    string Email,
    string Password) : IQuery<AuthenticationResult>;
