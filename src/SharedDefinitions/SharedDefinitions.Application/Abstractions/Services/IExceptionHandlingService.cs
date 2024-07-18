// <copyright file="IExceptionHandlingService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using Microsoft.Extensions.Logging;

namespace SharedDefinitions.Application.Abstractions.Services;

/// <summary>
/// Exception Handling Interface.
/// </summary>
public interface IExceptionHandlingService
{
    /// <summary>
    /// Handles the exception and returns a informational message.
    /// </summary>
    /// <param name="exception">Exception to be handled.</param>
    /// <param name="logger">Logger instance for logging the exception.</param>
    /// <returns>A result with the errors from the exception.</returns>
    Result HandleException(Exception exception, ILogger? logger = null);
}
