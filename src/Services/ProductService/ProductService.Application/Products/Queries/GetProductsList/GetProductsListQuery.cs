// <copyright file="GetProductsListQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;
using SneakerCollector.Services.ProductService.Application.Products.Dtos;

namespace SneakerCollector.Services.ProductService.Application.Products.Queries.GetProductsList;

/// <summary>
/// Gets the List of Products.
/// </summary>
public record GetProductsListQuery() : IQuery<List<ProductDto>>;
