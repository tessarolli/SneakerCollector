// <copyright file="InvalidPasswordError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Application.Common.Errors;

namespace AuthService.Application.Authentication.Errors;

/// <summary>
/// Invalid Password error.
/// </summary>
public class InvalidPasswordError : UnauthorizedError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPasswordError"/> class.
    /// </summary>
    public InvalidPasswordError()
    {
        Message = "Invalid Password";
    }
}
