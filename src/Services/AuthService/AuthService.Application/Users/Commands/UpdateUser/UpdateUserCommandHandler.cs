﻿// <copyright file="UpdateUserCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Users.Dtos;
using AuthService.Contracts.Enums;
using AuthService.Domain.Users;
using FluentResults;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Domain.Common.Abstractions;

namespace AuthService.Application.Users.Commands.UpdateUser;

/// <summary>
/// Mediator Handler for the <see cref="UpdateUserCommand"/>.
/// </summary>
/// <param name="userRepository">Injected _userRepository.</param>
/// <param name="passwordHasher">Injected _passwordHasher.</param>
public class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHashingService passwordHasher)
    : ICommandHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHashingService _passwordHasher = passwordHasher;

    /// <inheritdoc/>
    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userDomainModel = User.Create(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            (Roles)request.Role,
            passwordHasher: _passwordHasher);

        if (!userDomainModel.IsSuccess)
        {
            return Result.Fail(userDomainModel.Errors);
        }

        var updateResult = await _userRepository.UpdateAsync(userDomainModel.Value);
        if (!updateResult.IsSuccess)
        {
            return Result.Fail(updateResult.Errors);
        }

        return Result.Ok(new UserDto(
            updateResult.Value.Id.Value,
            updateResult.Value.FirstName,
            updateResult.Value.LastName,
            updateResult.Value.Email,
            updateResult.Value.Password.Value,
            (int)updateResult.Value.Role,
            updateResult.Value.CreatedAtUtc));
    }
}
