// <copyright file="DeleteUserCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Commands.DeleteUser;

/// <summary>
/// Command to Delete a User from the repository.
/// </summary>
/// <param name="Id">The Id of the User being deleted.</param>
public record DeleteUserCommand(long Id) : ICommand;
