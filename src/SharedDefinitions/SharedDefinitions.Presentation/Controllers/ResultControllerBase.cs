// <copyright file="ResultControllerBase.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Application.Common.Errors;
using SharedDefinitions.Domain.Common;

namespace SharedDefinitions.Presentation.Controllers;

/// <summary>
/// Base Class for Api Controller that Handles Validation Results.
/// </summary>
/// <typeparam name="TController">Type.</typeparam>
/// <param name="mediator">Injected _mediator.</param>
/// <param name="mapper">Injected _mapper.</param>
/// <param name="logger">ILogger injected.</param>
/// <param name="exceptionHandlingService">IExceptionHandlingService injected.</param>
[ApiController]
[Authorize]
public class ResultControllerBase<TController>(
    IMediator mediator,
    IMapper mapper,
    ILogger<TController> logger,
    IExceptionHandlingService exceptionHandlingService)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly IExceptionHandlingService _exceptionHandlingService = exceptionHandlingService;

    /// <summary>
    /// Gets the ILogger instance.
    /// </summary>
    protected ILogger Logger { get; init; } = logger;

    /// <summary>
    /// Handler for received requests.
    /// </summary>
    /// <typeparam name="TCommandOrQuery">The Type of the Command or Query to execute.</typeparam>
    /// <typeparam name="TDto">The Type of the Command or Query Result.</typeparam>
    /// <typeparam name="TResponse">The Type of the Response (contract).</typeparam>
    /// <param name="request">The input received in the request.</param>
    /// <param name="caller">The Name of the Method that Invoked this method.</param>
    /// <returns>An ActionResult for sending to the client.</returns>
    [NonAction]
    public async Task<IActionResult> HandleRequestAsync<TCommandOrQuery, TDto, TResponse>(object? request = null, [CallerMemberName] string caller = "")
    {
        Logger.LogInformation("{Caller}: {Request}", caller, request);

        Result<TDto> result;
        TResponse? response = default;

        try
        {
            // Create the command object based on the request type
            object command = CreateCommandFromRequest<TCommandOrQuery>(request);

            // Send command/query to mediator
            result = await SendCommandToMediator<TDto>(command);

            // Handle successful result
            if (result.IsSuccess)
            {
                // Map the result to the response type
                response = _mapper.Map<TResponse>(result.Value!);
            }
        }
        catch (Exception ex)
        {
            result = _exceptionHandlingService.HandleException(ex, Logger);
        }

        return ValidateResult(
               result,
               () => Ok(response),
               () => Problem());
    }

    /// <summary>
    /// Validates the result.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    /// <param name="result">FluentResult.</param>
    /// <param name="success">Action to perform when success.</param>
    /// <param name="failure">Action to perform when failure.</param>
    /// <returns>ActionResult according to Result.</returns>
    [NonAction]
    public ActionResult ValidateResult<T>(Result<T> result, Func<ActionResult> success, Action failure)
    {
        if (result.IsSuccess)
        {
            Logger.LogInformation("Request Success");

            return success.Invoke();
        }

        failure.Invoke();

        var errorMessages = result.Errors.Select(e => e.Message).ToList();

        Logger.LogInformation("Request Failure with message(s):\n{ErrorMessages}", errorMessages);

        var status = ResultControllerBase<TController>.GetStatusCode(result);

        var problemDetails = new ProblemDetails
        {
            Instance = HttpContext.Request.Path,
            Status = status.Item1,
            Title = status.Item2,
            Detail = "One or more erros ocurred.",
            Type = $"https://httpstatuses.com/{ResultControllerBase<TController>.GetStatusCode(result)}",
            Extensions = { { "errors", errorMessages } },
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
        };
    }

    /// <summary>
    /// Get Http Status Code from Result.
    /// </summary>
    /// <typeparam name="T">Result Type.</typeparam>
    /// <param name="result">instance.</param>
    /// <returns>Http Status code.</returns>
    private static (int, string) GetStatusCode<T>(Result<T> result)
    {
        if (result.HasError<ValidationError>())
        {
            return (400, "Validation Error");
        }

        if (result.HasError<UnauthorizedError>())
        {
            return (403, "You dont have permission to access this resource.");
        }

        if (result.HasError<NotFoundError>())
        {
            return (404, "Resource Not Found.");
        }

        if (result.HasError<ConflictError>())
        {
            return (409, "A resource with the same content already exists.");
        }

        return (500, "Internal Server Error");
    }

    private object CreateCommandFromRequest<TCommandOrQuery>(object? request)
    {
        if (request is null)
        {
            // Create command without parameters
            return Activator.CreateInstance(typeof(TCommandOrQuery))!;
        }

        // Create command based on request type
        return request switch
        {
            long idRequest => Activator.CreateInstance(typeof(TCommandOrQuery), idRequest)!,
            _ => _mapper.Map<TCommandOrQuery>(request)!
        };
    }

    private async Task<Result<TDto>> SendCommandToMediator<TDto>(object command)
    {
        return typeof(TDto) == typeof(Result)
            ? await _mediator.Send((IRequest<Result>)command)
            : await _mediator.Send((IRequest<Result<TDto>>)command);
    }
}
