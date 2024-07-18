// <copyright file="ConflictError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;

namespace SharedDefinitions.Application.Common.Errors;

/// <summary>
/// Not Found Error.
/// </summary>
public class ConflictError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictError"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public ConflictError(string message = "A resource with the same content already exists.")
        : base(message)
    {
    }
}
