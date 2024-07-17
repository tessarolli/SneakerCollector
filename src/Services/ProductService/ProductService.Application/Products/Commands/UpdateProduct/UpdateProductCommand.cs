// <copyright file="UpdateProductCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using SneakerCollector.Services.ProductService.Application.Products.Dtos;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.ProductService.Application.Products.Commands.UpdateProduct;

/// <summary>
/// Command to Update a Product in the catalog.
/// </summary>
/// <param name="Id">The Product's Id to update.</param>
/// <param name="OwnerId">The updated Product's Owner Id.</param>
/// <param name="Name">The updated Product's Name.</param>
/// <param name="Description">The updated Product's Description.</param>
/// <param name="BasePrice">The updated Product's Base Price.</param>
/// <param name="Stock">The updated Product's Quantity in Stock.</param>
public record UpdateProductCommand(
    long Id,
    long OwnerId,
    string Name,
    string Description,
    decimal BasePrice,
    int Stock) : ICommand<ProductDto>;
