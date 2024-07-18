// <copyright file="LoginRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace AuthService.Contracts.Authentication;

/// <summary>
/// Contract for the Login Request.
/// </summary>
/// <param name="Email">The User's E-mail</param>
/// <param name="Password">The User's Password</param>
public record struct LoginRequest(
    string Email,
    string Password);
