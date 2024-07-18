// <copyright file="UpdateBrandCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Brands.Dtos;
using ProductService.Domain.Brands.Enums;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.UpdateBrand;

/// <summary>
/// Command to Update a Brand in the catalog.
/// </summary>
/// <param name="BrandId">The Brand's Id.</param>
/// <param name="OwnerId">The Brand's Owner Id.</param>
/// <param name="BrandId">The Brand's Brand Id.</param>
/// <param name="BrandName">The Brand's Brand Name.</param>
/// <param name="Name">The Brand's Name.</param>
/// <param name="Currency">The Brand's Price Currency.</param>
/// <param name="Price">The Brand's Price Amount.</param>
/// <param name="SizeUnit">The Brand's Size Unit.</param>
/// <param name="Size">The Brand's Size.</param>
/// <param name="Year">The Brand's Launch Year.</param>
/// <param name="Rating">The Brand's Rating.</param>
public record UpdateBrandCommand(
    long BrandId,
    long OwnerId,
    long BrandId,
    string BrandName,
    string Name,
    Currency Currency,
    decimal Price,
    BrandSizeUnit SizeUnit,
    decimal Size,
    int Year,
    int Rating) : ICommand<BrandDto>;