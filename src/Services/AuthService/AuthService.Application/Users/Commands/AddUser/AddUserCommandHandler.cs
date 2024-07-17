// <copyright file="AddUserCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using FluentResults;
using SneakerCollector.Services.AuthService.Application.Abstractions.Repositories;
using SneakerCollector.Services.AuthService.Application.Users.Dtos;
using SneakerCollector.Services.AuthService.Domain.Users.ValueObjects;
using SneakerCollector.Services.AuthService.Domain.Users;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;
using SneakerCollector.SharedDefinitions.Domain.Common.Abstractions;

namespace SneakerCollector.Services.AuthService.Application.Users.Commands.AddUser;

/// <summary>
/// Mediator Handler for the <see cref="AddUserCommand"/>.
/// </summary>
public class AddUserCommandHandler : ICommandHandler<AddUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Injected UserRepository.</param>
    /// <param name="passwordHasher">Injected PasswordHasher.</param>
    public AddUserCommandHandler(IUserRepository userRepository, IPasswordHashingService passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

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
