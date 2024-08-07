﻿// <copyright file="GetUserByIdQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace AuthService.Application.Users.Queries.GetUserById;

/// <summary>
/// Validator for the <see cref="GetUserByIdQuery"/>.
/// </summary>
public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryValidator"/> class.
    /// </summary>
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0);
    }
}