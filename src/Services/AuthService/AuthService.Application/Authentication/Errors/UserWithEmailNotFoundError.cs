// <copyright file="UserWithEmailNotFoundError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Application.Common.Errors;

namespace AuthService.Application.Authentication.Errors;

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
