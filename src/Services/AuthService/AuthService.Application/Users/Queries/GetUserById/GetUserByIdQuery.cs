// <copyright file="GetUserByIdQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.Services.AuthService.Application.Users.Dtos;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.AuthService.Application.Users.Queries.GetUserById;

/// <summary>
/// Gets the User by its Id.
/// </summary>
public record GetUserByIdQuery(long Id) : IQuery<UserDto>;
