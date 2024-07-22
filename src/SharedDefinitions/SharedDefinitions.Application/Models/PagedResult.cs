// <copyright file="PagedResult.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Application.Models;/// <summary>/// Represents a generic class for storing paged results of type T./// </summary>/// <typeparam name="T">The Type for the Items.</typeparam>public sealed class PagedResult<T>    where T : class{
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
    public int Limit { get; init; }}