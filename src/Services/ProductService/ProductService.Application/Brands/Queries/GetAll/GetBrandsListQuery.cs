// <copyright file="GetBrandsListQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Brands.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Application.Models;

namespace ProductService.Application.Brands.Queries.GetAll;

/// <summary>
/// Gets the List of Brands.
/// </summary>
/// <param name="Request">Pagination, Sorting and Filtering Request.</param>
public record GetBrandsListQuery(
    PagedAndSortedResultRequest Request) : IQuery<PagedResult<BrandDto>>;