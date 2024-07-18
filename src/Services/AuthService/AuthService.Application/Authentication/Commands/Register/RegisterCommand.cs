// <copyright file="RegisterCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Authentication.Results;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Authentication.Commands.Register;

/// <summary>
/// Contract for the Register Command
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<AuthenticationResult>;
