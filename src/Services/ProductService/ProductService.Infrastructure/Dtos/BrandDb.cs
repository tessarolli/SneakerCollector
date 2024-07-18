// <copyright file="BrandDb.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Infrastructure.Dtos;

/// <summary>
/// Brand model as a representation of the database schema.
/// </summary>
public record BrandDb(
    int id,
    string name);