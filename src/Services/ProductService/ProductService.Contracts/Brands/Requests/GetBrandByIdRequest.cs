// <copyright file="GetBrandByIdRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Brands.Requests;

/// <summary>
/// A request to fetch a Brand by its Id.
/// </summary>
/// <param name="Id">The Brand's Id.</param>
public record GetBrandByIdRequest(long Id);