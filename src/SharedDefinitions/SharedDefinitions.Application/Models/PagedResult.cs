// <copyright file="PagedResult.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Application.Models;/// <summary>
/// Represents a generic class for storing paged results of type T.
/// </summary>
/// <typeparam name="T">The Type for the Items.</typeparam>
public sealed class PagedResult<T>
    where T : class{
    /// <summary>
    /// Gets the number of items.
    /// </summary>
    public IEnumerable<T> Items { get; init; } = [];

    /// <summary>
    /// Gets the page number.
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Gets the page size for pagination.
    /// </summary>
    public int PageSize { get; init; }}