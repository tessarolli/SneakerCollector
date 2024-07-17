// <copyright file="GetProductByIdQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using SneakerCollector.Services.ProductService.Application.Products.Dtos;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.ProductService.Application.Products.Queries.GetProductById;

/// <summary>
/// Gets the Product by its Id.
/// </summary>
/// <param name="Id">The Id of the product being deleted.</param>
public record GetProductByIdQuery(long Id) : IQuery<ProductDto>;
