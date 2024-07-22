// <copyright file="PagedAndSortedResultRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Application.Models;/// <summary>/// Represents a request for paged and sorted results./// </summary>public class PagedAndSortedResultRequest{
    /// <summary>
    /// Gets or sets the offset value.
    /// </summary>
    public int Offset { get; set; } = 0;

    /// <summary>
    /// Gets or sets the limit value, with a default value of 20.
    /// </summary>
    public int Limit { get; set; } = 20;

    /// <summary>
    /// Gets or sets the property used for sorting. Default value is an empty string.
    /// </summary>
    public string Sort { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filters for a specific operation.
    /// Default value is an empty string.
    /// </summary>
    public string Search { get; set; } = string.Empty;}