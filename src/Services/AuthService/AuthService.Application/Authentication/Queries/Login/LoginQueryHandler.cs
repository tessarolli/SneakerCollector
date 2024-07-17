﻿// <copyright file="LoginQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.Services.AuthService.Application.Abstractions.Authentication;
using SneakerCollector.Services.AuthService.Application.Abstractions.Repositories;
using SneakerCollector.Services.AuthService.Application.Authentication.Errors;
using SneakerCollector.Services.AuthService.Application.Authentication.Results;
using FluentResults;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;
using SneakerCollector.SharedDefinitions.Domain.Common.Abstractions;
using SneakerCollector.Services.AuthService.Domain.Users;

namespace SneakerCollector.Services.AuthService.Application.Authentication.Queries.Login;

/// <summary>
/// Implementation for the Login Query.
/// </summary>
public class LoginQueryHandler : IQueryHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Injected UserRepository.</param>
    /// <param name="jwtTokenGenerator">Injected JwtTokenGenerator.</param>
    /// <param name="passwordHasher">Injected PasswordHasher.</param>
    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHashingService passwordHasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

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