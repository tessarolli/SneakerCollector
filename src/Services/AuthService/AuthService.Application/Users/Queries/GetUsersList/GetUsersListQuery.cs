// <copyright file="GetUsersListQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using SneakerCollector.Services.AuthService.Application.Users.Dtos;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.AuthService.Application.Users.Queries.GetUsersList;

/// <summary>
/// Gets the List of Users.
/// </summary>
public record GetUsersListQuery() : IQuery<List<UserDto>>;
