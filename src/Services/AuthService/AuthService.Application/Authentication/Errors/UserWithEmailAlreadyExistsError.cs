// <copyright file="UserWithEmailAlreadyExistsError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.SharedDefinitions.Application.Common.Errors;

namespace SneakerCollector.Services.AuthService.Application.Authentication.Errors;

/// <summary>
/// User with this email already exists.
/// </summary>
public class UserWithEmailAlreadyExistsError : ConflictError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserWithEmailAlreadyExistsError"/> class.
    /// </summary>
    public UserWithEmailAlreadyExistsError()
    {
        Message = "Given E-Mail address is already in use";
    }
}
