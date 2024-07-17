// <copyright file="AuthenticationController.cs" company="SneakerCollector">
// Copyright (c) AuthService. All rights reserved.
// </copyright>

using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerCollector.Services.AuthService.Application.Authentication.Commands.Register;
using SneakerCollector.Services.AuthService.Application.Authentication.Queries.Login;
using SneakerCollector.Services.AuthService.Application.Authentication.Results;
using SneakerCollector.Services.AuthService.Contracts.Authentication;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Services;
using SneakerCollector.SharedDefinitions.Presentation.Controllers;

namespace SneakerCollector.Services.AuthService.API.Controllers;

/// <summary>
/// Authentication Controller.
/// </summary>
[Route("authentication")]
public class AuthenticationController : ResultControllerBase<AuthenticationController>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    /// <param name="mediator">Injected _mediator.</param>
    /// <param name="mapper">Injected _mapper.</param>
    /// <param name="logger">Injected _logger.</param>
    /// <param name="exceptionHandlingService">Injected _exceptionHandlingService.</param>
    public AuthenticationController(IMediator mediator, IMapper mapper, ILogger<AuthenticationController> logger, IExceptionHandlingService exceptionHandlingService)
        : base(mediator, mapper, logger, exceptionHandlingService)
    {
    }

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
