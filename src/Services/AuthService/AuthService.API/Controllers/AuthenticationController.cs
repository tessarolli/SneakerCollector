// <copyright file="AuthenticationController.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Authentication.Commands.Register;
using AuthService.Application.Authentication.Queries.Login;
using AuthService.Application.Authentication.Results;
using AuthService.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Presentation.Controllers;

namespace AuthService.API.Controllers;

/// <summary>
/// Authentication Controller.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AuthenticationController"/> class.
/// </remarks>
/// <param name="mediator">Injected _mediator.</param>
/// <param name="mapper">Injected _mapper.</param>
/// <param name="logger">Injected Logger.</param>
/// <param name="exceptionHandlingService">Injected _exceptionHandlingService.</param>
[Route("authentication")]
public class AuthenticationController(
    IMediator mediator,
    IMapper mapper,
    ILogger<AuthenticationController> logger,
    IExceptionHandlingService exceptionHandlingService)
    : ResultControllerBase<AuthenticationController>(mediator, mapper, logger, exceptionHandlingService)
{
    /// <summary>
    /// Endpoint to register an user.
    /// </summary>
    /// <param name="request">User data for registration.</param>
    /// <returns>The result of the register operation.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request) =>
         await HandleRequestAsync<RegisterCommand, AuthenticationResult, AuthenticationResponse>(request);

    /// <summary>
    /// Endpoint for a user to perform Login.
    /// </summary>
    /// <param name="request">User data for login.</param>
    /// <returns>The result of the login operation.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginRequest request) =>
       await HandleRequestAsync<LoginQuery, AuthenticationResult, AuthenticationResponse>(request);
}