// <copyright file="UserDto.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Domain.Dtos;

/// <summary>
/// Data Transfer Object for the User Domain Model.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or Sets the User's Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or Sets the User's First Name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or Sets the User's Last Name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or Sets the User's E-Mail.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or Sets the User's Role.
    /// </summary>
    public int Role { get; set; } = 0;
}
