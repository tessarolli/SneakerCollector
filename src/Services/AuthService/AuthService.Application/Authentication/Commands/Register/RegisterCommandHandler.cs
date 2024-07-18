// <copyright file="RegisterCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Authentication;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Authentication.Errors;
using AuthService.Application.Authentication.Results;
using AuthService.Domain.Users;
using FluentResults;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Application.Common.Errors;
using SharedDefinitions.Domain.Common.Abstractions;

namespace AuthService.Application.Authentication.Commands.Register;

/// <summary>
/// The implementation for the Register Command.
/// </summary>
/// <param name="userRepository">IUserRepository being injected.</param>
/// <param name="jwtTokenGenerator">IJwtTokenGenerator being injected.</param>
/// <param name="passwordHasher">IPasswordHashingService being injected.</param>
public class RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHashingService passwordHasher) : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHashingService _passwordHasher = passwordHasher;

    /// <summary>
    /// The actual command Handler implementation for registering a new user.
    /// </summary>
    /// <param name="command">RegisterCommand.</param>
    /// <param name="cancellationToken">Async CancellationToken.</param>
    /// <returns>FluentResult for the operation.</returns>
    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check if a user with the given e-mail already exists
        // Although this is a business rule, it's more of an application complexity than a domain complexity
        // Therefore, we can handle this rule enforcement here in the application layer.
        var getUserByEmailResult = await _userRepository.GetByEmailAsync(command.Email);
        if (getUserByEmailResult.IsFailed)
        {
            if (!getUserByEmailResult.HasError<NotFoundError>())
            {
                return Result.Fail(getUserByEmailResult.Errors);
            }
        }
        else
        {
            return Result.Fail(new UserWithEmailAlreadyExistsError());
        }

        // Now that we Ensure the Business Rule, we can carry on with the User Creation
        // and persisting it to the repository.
        var userResult = User.Create(null, command.FirstName, command.LastName, command.Email, command.Password, passwordHasher: _passwordHasher);
        if (userResult.IsFailed)
        {
            return Result.Fail(userResult.Errors);
        }

        // Persist newly created User Entity Instance to the Repository
        var persistResult = await _userRepository.AddAsync(userResult.Value);
        if (persistResult.IsFailed)
        {
            return Result.Fail(persistResult.Errors);
        }

        // Generate Token
        var token = _jwtTokenGenerator.GenerateToken(persistResult.Value);

        return new AuthenticationResult(persistResult.Value, token);
    }
}