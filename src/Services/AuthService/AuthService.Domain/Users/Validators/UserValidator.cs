// <copyright file="UserValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Domain.Users.ValueObjects;
using FluentValidation;
using SharedDefinitions.Domain.Common;

namespace AuthService.Domain.Users.Validators;

/// <summary>
/// User Entity Validation Rules.
/// </summary>
public sealed class UserValidator : EntityValidator<UserId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserValidator"/> class.
    /// </summary>
    public UserValidator()
    {
        RuleFor(g => ((User)g).FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(g => ((User)g).LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(g => ((User)g).Email)
            .EmailAddress();
    }
}
