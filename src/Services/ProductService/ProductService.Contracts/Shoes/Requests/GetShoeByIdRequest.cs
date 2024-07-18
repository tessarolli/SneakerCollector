// <copyright file="GetShoeByIdRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Shoes.Requests;

/// <summary>
/// A request to fetch a Shoe by its Id.
/// </summary>
/// <param name="Id">The Shoe's Id.</param>
public record GetShoeByIdRequest(long Id);