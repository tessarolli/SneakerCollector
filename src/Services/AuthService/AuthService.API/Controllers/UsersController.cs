// <copyright file="UsersController.cs" company="SneakerCollector">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SneakerCollector.Services.AuthService.Application.Users.Commands.AddUser;
using SneakerCollector.Services.AuthService.Application.Users.Commands.DeleteUser;
using SneakerCollector.Services.AuthService.Application.Users.Commands.UpdateUser;
using SneakerCollector.Services.AuthService.Application.Users.Dtos;
using SneakerCollector.Services.AuthService.Application.Users.Queries.GetUserById;
using SneakerCollector.Services.AuthService.Application.Users.Queries.GetUsersList;
using SneakerCollector.Services.AuthService.Contracts.Enums;
using SneakerCollector.Services.AuthService.Contracts.User.Requests;
using SneakerCollector.Services.AuthService.Contracts.User.Responses;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Services;
using SneakerCollector.SharedDefinitions.Presentation.Attributes;
using SneakerCollector.SharedDefinitions.Presentation.Controllers;

namespace SneakerCollector.Services.AuthService.API.Controllers;

/// <summary>
/// Users Controller.
/// </summary>
[Route("[controller]")]
public class UsersController : ResultControllerBase<UsersController>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="mediator">Injected _mediator.</param>
    /// <param name="mapper">Injected _mapper.</param>
    /// <param name="logger">Injected _logger.</param>
    /// <param name="exceptionHandlingService">Injected _exceptionHandlingService.</param>
    public UsersController(IMediator mediator, IMapper mapper, ILogger<UsersController> logger, IExceptionHandlingService exceptionHandlingService)
        : base(mediator, mapper, logger, exceptionHandlingService)
    {
    }

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
