﻿// <copyright file="NotFoundError.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;

namespace SharedDefinitions.Application.Common.Errors;

/// <summary>
/// Not Found Error.
/// </summary>
public class NotFoundError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundError"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public NotFoundError(string message = "Resource Not Found.")
        : base(message)
    {
    }
}
