// <copyright file="AddUserCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Users.Dtos;
using AuthService.Domain.Users;
using FluentResults;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Domain.Common.Abstractions;

namespace AuthService.Application.Users.Commands.AddUser;

/// <summary>
/// Mediator Handler for the <see cref="AddUserCommand"/>.
/// </summary>
/// <param name="userRepository">Injected UserRepository.</param>
/// <param name="passwordHasher">Injected PasswordHasher.</param>
public class AddUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHashingService passwordHasher)
    : ICommandHandler<AddUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHashingService _passwordHasher = passwordHasher;

    /// <inheritdoc/>
    public async Task<Result<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var userDomainModel = User.Create(
            null,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            passwordHasher: _passwordHasher);

        if (!userDomainModel.IsSuccess)
        {
            return Result.Fail(userDomainModel.Errors);
        }

        var addResult = await _userRepository.AddAsync(userDomainModel.Value);
        if (!addResult.IsSuccess)
        {
            return Result.Fail(addResult.Errors);
        }

        return Result.Ok(new UserDto(
            addResult.Value.Id.Value,
            addResult.Value.FirstName,
            addResult.Value.LastName,
            addResult.Value.Email,
            addResult.Value.Password.HashedPassword,
            (int)addResult.Value.Role,
            addResult.Value.CreatedAtUtc));
    }
}
