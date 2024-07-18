// <copyright file="UnreachableExternalServiceError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;

namespace SharedDefinitions.Application.Common.Errors;

/// <summary>
/// Failure to access an external service error.
/// </summary>
public class UnreachableExternalServiceError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnreachableExternalServiceError"/> class.
    /// </summary>
    /// <param name="serviceName">The name of the micro service.</param>
    public UnreachableExternalServiceError(string serviceName = "")
    {
        Message = $"A required external service was not reachable {(string.IsNullOrWhiteSpace(serviceName) ? string.Empty : $"({serviceName})")}";
    }
}