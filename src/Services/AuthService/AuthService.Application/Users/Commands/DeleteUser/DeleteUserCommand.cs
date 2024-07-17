// <copyright file="DeleteUserCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.AuthService.Application.Users.Commands.DeleteUser;

/// <summary>
/// Command to Delete a User from the repository.
/// </summary>
/// <param name="Id">The Id of the User being deleted.</param>
public record DeleteUserCommand(long Id) : ICommand;
