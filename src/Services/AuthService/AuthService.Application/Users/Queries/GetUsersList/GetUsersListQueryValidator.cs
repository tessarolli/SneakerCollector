// <copyright file="GetUsersListQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using FluentValidation;

namespace SneakerCollector.Services.AuthService.Application.Users.Queries.GetUsersList;

/// <summary>
/// Validator.
/// </summary>
public class GetUsersListQueryValidator : AbstractValidator<GetUsersListQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersListQueryValidator"/> class.
    /// </summary>
    public GetUsersListQueryValidator()
    {
    }
}
