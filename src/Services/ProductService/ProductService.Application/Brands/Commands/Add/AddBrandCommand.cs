// <copyright file="AddBrandCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Brands.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.Add;

/// <summary>
/// Command to Add a Brand to the catalog.
/// </summary>
/// <param name="BrandId">The Brand's Id.</param>
/// <param name="BrandName">The Brand's Name.</param>
public record AddBrandCommand(
    long BrandId,
    string BrandName) : ICommand<BrandDto>;