// <copyright file="GetProductsListQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Shoes.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Queries.GetShoesList;

/// <summary>
/// Gets the List of Products.
/// </summary>
public record GetProductsListQuery() : IQuery<List<ShoeDto>>;
