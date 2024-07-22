﻿// <copyright file="PagedResult.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Application.Models;
    /// <summary>
    /// Gets a collection of items of type T.
    /// </summary>
    public IEnumerable<T> Items { get; init; } = [];

    /// <summary>
    /// Gets the total count of items.
    /// </summary>
    public int TotalCount { get; init; }

    /// <summary>
    /// Gets the offset value.
    /// </summary>
    public int Offset { get; init; }

    /// <summary>
    /// Gets the limit value for a property.
    /// </summary>
    public int Limit { get; init; }