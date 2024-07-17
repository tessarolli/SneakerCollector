// <copyright file="GetProductByIdRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

namespace SneakerCollector.Services.ProductService.Contracts.Product.Requests;

/// <summary>
/// A request to fetch a Product by its Id.
/// </summary>
/// <param name="Id">The Product's Id.</param>
public record GetProductByIdRequest(long Id);
