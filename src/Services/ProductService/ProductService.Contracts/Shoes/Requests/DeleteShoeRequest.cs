// <copyright file="DeleteShoeRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Shoes.Requests;

/// <summary>
/// A request to delete a Shoe from the repository.
/// </summary>
/// <param name="Id">The Shoe's Id.</param>
public record DeleteShoeRequest(long Id);