// <copyright file="UsersController.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Users.Commands.AddUser;
using AuthService.Application.Users.Commands.DeleteUser;
using AuthService.Application.Users.Commands.UpdateUser;
using AuthService.Application.Users.Dtos;
using AuthService.Application.Users.Queries.GetUserById;
using AuthService.Application.Users.Queries.GetUsersList;
using AuthService.Contracts.Enums;
using AuthService.Contracts.User.Requests;
using AuthService.Contracts.User.Responses;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Presentation.Attributes;
using SharedDefinitions.Presentation.Controllers;

namespace AuthService.API.Controllers;

/// <summary>
/// Users Controller.
/// </summary>
/// <param name="mediator">Injected _mediator.</param>
/// <param name="mapper">Injected _mapper.</param>
/// <param name="logger">Injected Logger.</param>
/// <param name="exceptionHandlingService">Injected _exceptionHandlingService.</param>
[Route("[controller]")]
public class UsersController(
    IMediator mediator,
    IMapper mapper,
    ILogger<UsersController> logger,
    IExceptionHandlingService exceptionHandlingService)
    : ResultControllerBase<UsersController>(mediator, mapper, logger, exceptionHandlingService)
{
    /// <summary>
    /// Gets a list of Users.
    /// </summary>
    /// <returns>The list of Users.</returns>
    [HttpGet]
    [RoleAuthorize]
    public async Task<IActionResult> GetUsers()
        => await HandleRequestAsync<GetUsersListQuery, List<UserDto>, List<UserResponse>>();

    /// <summary>
    /// Gets a User by its Id.
    /// </summary>
    /// <param name="id">User Id.</param>
    /// <returns>The User Aggregate.</returns>
    [HttpGet("{id:long}")]
    [RoleAuthorize]
    public async Task<IActionResult> GetUserById(long id)
        => await HandleRequestAsync<GetUserByIdQuery, UserDto, UserResponse>(id);

    /// <summary>
    /// Add a User to the User Repository.
    /// </summary>
    /// <param name="request">User data.</param>
    /// <returns>The User instance created with Id.</returns>
    [HttpPost]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> AddUser(AddUserRequest request)
        => await HandleRequestAsync<AddUserCommand, UserDto, UserResponse>(request);

    /// <summary>
    /// Updates a User in the User Repository.
    /// </summary>
    /// <param name="request">User data.</param>
    /// <returns>The User instance updated with Id.</returns>
    [HttpPut]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        => await HandleRequestAsync<UpdateUserCommand, UserDto, UserResponse>(request);

    /// <summary>
    /// Deletes a User from the User Repository.
    /// </summary>
    /// <param name="id">User Id.</param>
    /// <returns>The Action Result of the delete operation.</returns>
    [HttpDelete("{id:long}")]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> DeleteUser(long id)
        => await HandleRequestAsync<DeleteUserCommand, Result, object>(id);
}