// <copyright file="LoginQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Authentication;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Authentication.Errors;
using AuthService.Application.Authentication.Results;
using AuthService.Domain.Users;
using FluentResults;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Domain.Common.Abstractions;

namespace AuthService.Application.Authentication.Queries.Login;

/// <summary>
/// Implementation for the Login Query.
/// </summary>
/// <param name="userRepository">Injected UserRepository.</param>
/// <param name="jwtTokenGenerator">Injected JwtTokenGenerator.</param>
/// <param name="passwordHasher">Injected PasswordHasher.</param>
public class LoginQueryHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHashingService passwordHasher) : IQueryHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHashingService _passwordHasher = passwordHasher;

    /// <inheritdoc/>
    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Check if User with given e-mail already exists
        Result<User> userResult = await _userRepository.GetByEmailAsync(query.Email);
        if (userResult.IsFailed)
        {
            return Result.Fail(userResult.Errors);
        }

        // Validate the Password
        if (!_passwordHasher.VerifyPassword(query.Password, userResult.Value.Password.Value))
        {
            return Result.Fail(new InvalidPasswordError());
        }

        // Generate Token
        var token = _jwtTokenGenerator.GenerateToken(userResult.Value);

        return new AuthenticationResult(userResult.Value, token);
    }
}