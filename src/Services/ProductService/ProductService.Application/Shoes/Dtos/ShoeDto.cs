// <copyright file="ShoeDto.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Domain.Shoes.Enums;

namespace ProductService.Application.Shoes.Dtos;

/// <summary>
/// Contract for the Shoe Data Transfer Object.
/// </summary>
public record ShoeDto(
    long Id,
    long OwnerId,
    long BrandId,
    string Name,
    Currency Currency,
    decimal Price,
    ShoeSizeUnit SizeUnit,
    decimal Size,
    DateTime CreatedAtUtc);