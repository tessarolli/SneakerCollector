// <copyright file="GetBrandsListQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Brands.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Queries.GetBrandsList;

/// <summary>
/// Gets the List of Products.
/// </summary>
public record GetBrandsListQuery() : IQuery<List<BrandDto>>;
