// <copyright file="UpdateBrandRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Brands.Requests;

/// <summary>
/// A request to update the Brand in the repository.
/// </summary>
/// <param name="BrandId">The Brand's Brand Id.</param>
/// <param name="BrandName">The Brand's Brand Name.</param>
public record UpdateBrandRequest(
    long BrandId,
    string BrandName);