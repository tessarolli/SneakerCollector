// <copyright file="UpdateUserCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Users.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Commands.UpdateUser;

/// <summary>
/// Command to Update a User in the repository.
/// </summary>
public record UpdateUserCommand(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int Role) : ICommand<UserDto>;
