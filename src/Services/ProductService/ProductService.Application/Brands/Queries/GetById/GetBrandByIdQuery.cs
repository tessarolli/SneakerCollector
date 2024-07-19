// <copyright file="GetBrandByIdQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Brands.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Queries.GetById;

/// <summary>
/// Gets the Brand by its Id.
/// </summary>
/// <param name="Id">The Id of the brand being deleted.</param>
public record GetBrandByIdQuery(long Id) : IQuery<BrandDto>;