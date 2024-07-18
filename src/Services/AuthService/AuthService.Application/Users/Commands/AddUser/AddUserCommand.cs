// <copyright file="AddUserCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Users.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Commands.AddUser;

/// <summary>
/// Command to Add a User to the repository.
/// </summary>
public record AddUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<UserDto>;
