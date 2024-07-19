// <copyright file="BrandResponse.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Brands.Responses;

/// <summary>
/// The Contract for Brand Response.
/// Can be used in collections.
/// </summary>
/// <param name="Id">The Brand's Id.</param>
/// <param name="Name">The Brand's Name.</param>
public record BrandResponse(
    long Id,
    string Name);