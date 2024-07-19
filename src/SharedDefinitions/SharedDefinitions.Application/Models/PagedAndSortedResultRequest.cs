// <copyright file="PagedAndSortedResultRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Application.Models;/// <summary>
/// Represents a request for paged and sorted results.
/// </summary>
public class PagedAndSortedResultRequest{
    /// <summary>
    /// Gets or sets the current page number, default value is 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size for pagination, default value is 20.
    /// </summary>
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// Gets or sets the field used for sorting.
    /// </summary>
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sort order for a collection or list.
    /// </summary>
    public string SortOrder { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a dictionary of filters where the
    /// key is a string representing the filter name and the
    /// value is a string representing the filter value.
    /// </summary>
    public Dictionary<string, string> Filters { get; set; } = [];}