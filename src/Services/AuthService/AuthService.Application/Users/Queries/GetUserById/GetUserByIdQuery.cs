// <copyright file="GetUserByIdQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Users.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Queries.GetUserById;

/// <summary>
/// Gets the User by its Id.
/// </summary>
public record GetUserByIdQuery(long Id) : IQuery<UserDto>;
