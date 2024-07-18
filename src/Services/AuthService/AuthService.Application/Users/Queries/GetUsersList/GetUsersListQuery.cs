// <copyright file="GetUsersListQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Users.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Queries.GetUsersList;

/// <summary>
/// Gets the List of Users.
/// </summary>
public record GetUsersListQuery() : IQuery<List<UserDto>>;
