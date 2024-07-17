// <copyright file="AddUserCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.Services.AuthService.Application.Users.Dtos;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.AuthService.Application.Users.Commands.AddUser;

/// <summary>
/// Command to Add a User to the repository.
/// </summary>
public record AddUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<UserDto>;
