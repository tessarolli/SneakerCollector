// <copyright file="GetShoeByIdQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Shoes.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Queries.GetShoeById;

/// <summary>
/// Gets the Shoe by its Id.
/// </summary>
/// <param name="Id">The Id of the shoe being deleted.</param>
public record GetShoeByIdQuery(long Id) : IQuery<ShoeDto>;