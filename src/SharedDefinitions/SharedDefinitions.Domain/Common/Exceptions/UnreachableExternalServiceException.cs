// <copyright file="UnreachableExternalServiceException.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Domain.Common.Exceptions;

/// <summary>
/// Exception thrown when an external service cant be accessed.
/// </summary>
/// <param name="serviceName">The external service Name.</param>
public class UnreachableExternalServiceException(string serviceName = "")
    : Exception(serviceName)
{
}