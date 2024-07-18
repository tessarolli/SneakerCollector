// <copyright file="AuthenticatedUserDto.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Application.Dtos;

/// <summary>
/// Data Transfer Object for the Authenticated user.
/// </summary>
/// <param name="Id">The Authenticated User Id.</param>
/// <param name="FirstName">The Authenticated User First Name.</param>
/// <param name="LastName">The Authenticated User Last Name.</param>
/// <param name="Email">The Authenticated User Email.</param>
/// <param name="Role">The Authenticated User Role.</param>
public record AuthenticatedUserDto(
    long? Id,
    string FirstName,
    string LastName,
    string Email,
    int Role);