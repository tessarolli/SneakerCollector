// <copyright file="GetUserByIdRequest.cs" company="SneakerCollector">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

namespace SneakerCollector.Services.AuthService.Contracts.User.Requests;

/// <summary>
/// A request to fetch a User by its Id.
/// </summary>
/// <param name="Id">The User's Id.</param>
public record GetUserByIdRequest(long Id);
