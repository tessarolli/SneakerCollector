// <copyright file="UserDb.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace AuthService.Infrastructure.Dtos;

/// <summary>
/// User model as a representation of the database schema.
/// </summary>
public record UserDb(
    long id,
    string first_name,
    string last_name,
    string email,
    string password,
    int role,
    DateTime created_at_utc);