// <copyright file="UpdateUserCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.Services.AuthService.Application.Users.Dtos;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.AuthService.Application.Users.Commands.UpdateUser;

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
