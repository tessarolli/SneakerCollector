// <copyright file="UserWithEmailAlreadyExistsError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Application.Common.Errors;

namespace AuthService.Application.Authentication.Errors;

/// <summary>
/// User with this email already exists error.
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
