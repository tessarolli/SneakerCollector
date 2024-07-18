// <copyright file="BrandDto.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Application.Brands.Dtos;

/// <summary>
/// Contract for the Brand Data Transfer Object.
/// </summary>
public record BrandDto(
    long Id,
    string Name);