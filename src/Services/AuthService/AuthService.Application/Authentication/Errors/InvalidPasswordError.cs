// <copyright file="InvalidPasswordError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.SharedDefinitions.Application.Common.Errors;

namespace SneakerCollector.Services.AuthService.Application.Authentication.Errors;

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
