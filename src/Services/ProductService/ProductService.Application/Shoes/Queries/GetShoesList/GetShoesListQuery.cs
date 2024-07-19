// <copyright file="GetShoesListQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Shoes.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Application.Models;

namespace ProductService.Application.Shoes.Queries.GetShoesList;

/// <summary>
/// Gets the List of Shoes.
/// </summary>
/// <param name="Request">Pagination, Sorting and Filtering Request.</param>
public record GetShoesListQuery(
    PagedAndSortedResultRequest Request)
    : IQuery<PagedResult<ShoeDto>>;
