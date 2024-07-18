// <copyright file="DeleteProductRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Product.Requests;

/// <summary>
/// A request to delete a Product from the repository.
/// </summary>
/// <param name="Id">The Product's Id.</param>
public record DeleteProductRequest(long Id);
