// <copyright file="AddProductRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Product.Requests;

/// <summary>
/// A request to Add a Product to the repository.
/// </summary>
/// <param name="Name">The Product's Name.</param>
/// <param name="OwnerId">The Product's OwnerId.</param>
/// <param name="Description">The Product's Description.</param>
/// <param name="BasePrice">The Product's Price.</param>
public record struct AddProductRequest(
    long OwnerId,
    string Name,
    string Description,
    decimal BasePrice);
