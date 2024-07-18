// <copyright file="GetProductByIdRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Product.Requests;

/// <summary>
/// A request to fetch a Product by its Id.
/// </summary>
/// <param name="Id">The Product's Id.</param>
public record GetProductByIdRequest(long Id);
