// <copyright file="LoginQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace AuthService.Application.Authentication.Queries.Login;

/// <summary>
/// Validation Rules for the Login Query.
/// </summary>
public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginQueryValidator"/> class.
    /// </summary>
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}