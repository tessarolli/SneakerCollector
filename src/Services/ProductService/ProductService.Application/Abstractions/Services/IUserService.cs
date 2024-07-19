// <copyright file="IUserService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Domain.Dtos;namespace ProductService.Application.Abstractions.Services;/// <summary>
/// Interface for a user service that defines operations related to users.
/// </summary>
public interface IUserService{
    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The id of the user.</param>
    /// <returns>Awaitable Task with UserDto if found.</returns>
    Task<UserDto?> GetUserByIdAsync(long id);}