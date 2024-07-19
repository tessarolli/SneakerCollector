// <copyright file="AddBrandRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Brands.Requests;

/// <summary>
/// Request to Add a Brand to the catalog.
/// </summary>
/// <param name="BrandName">The Brand's Brand Name.</param>
public record AddBrandRequest(string BrandName);