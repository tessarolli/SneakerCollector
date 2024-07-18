// <copyright file="AddUserCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace AuthService.Application.Users.Commands.AddUser;

/// <summary>
/// Validator for the <see cref="AddUserCommand"/>.
/// </summary>
public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddUserCommandValidator"/> class.
    /// </summary>
    public AddUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}