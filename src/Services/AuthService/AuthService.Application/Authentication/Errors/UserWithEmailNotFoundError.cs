// <copyright file="UserWithEmailNotFoundError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.SharedDefinitions.Application.Common.Errors;

namespace SneakerCollector.Services.AuthService.Application.Authentication.Errors;

/// <summary>
/// User with informed email does not exists.
/// </summary>
public class UserWithEmailNotFoundError : NotFoundError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserWithEmailNotFoundError"/> class.
    /// </summary>
    public UserWithEmailNotFoundError()
    {
        Message = "Account with given e-mail address does not exist";
    }
}
