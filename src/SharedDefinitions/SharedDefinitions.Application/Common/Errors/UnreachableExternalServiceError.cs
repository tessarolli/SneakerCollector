﻿// <copyright file="ExternalServiceError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using FluentResults;

namespace SneakerCollector.SharedDefinitions.Application.Common.Errors;

/// <summary>
/// Failure to access an external service error.
/// </summary>
public class UnreachableExternalServiceError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnreachableExternalServiceError"/> class.
    /// </summary>
    public UnreachableExternalServiceError(string serviceName = "")
    {
        Message = $"A required external service was not reachable {(string.IsNullOrWhiteSpace(serviceName) ? "" : $"({serviceName})")}";
    }
}
