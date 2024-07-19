// <copyright file="UpdateBrandCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Brands.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.Update;

/// <summary>
/// Command to Update a Brand in the catalog.
/// </summary>
/// <param name="BrandId">The Brand's Id.</param>
/// <param name="BrandName">The Brand's Brand Name.</param>
public record UpdateBrandCommand(
    long BrandId,
    string BrandName) : ICommand<BrandDto>;