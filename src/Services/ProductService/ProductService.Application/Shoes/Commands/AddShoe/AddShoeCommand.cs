// <copyright file="AddShoeCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Application.Shoes.Dtos;
using ProductService.Domain.Shoes.Enums;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Commands.AddShoe;

/// <summary>
/// Command to Add a Shoe to the catalog.
/// </summary>
/// <param name="OwnerId">The Shoe's Owner Id.</param>
/// <param name="BrandId">The Shoe's Brand Id.</param>
/// <param name="BrandName">The Shoe's Brand Name.</param>
/// <param name="Name">The Shoe's Name.</param>
/// <param name="Currency">The Shoe's Price Currency.</param>
/// <param name="Price">The Shoe's Price Amount.</param>
/// <param name="SizeUnit">The Shoe's Size Unit.</param>
/// <param name="Size">The Shoe's Size.</param>
/// <param name="Year">The Shoe's Launch Year.</param>
/// <param name="Rating">The Shoe's Rating.</param>
public record AddShoeCommand(
    long OwnerId,
    long BrandId,
    string BrandName,
    string Name,
    Currency Currency,
    decimal Price,
    ShoeSizeUnit SizeUnit,
    decimal Size,
    int Year,
    int Rating) : ICommand<ShoeDto>;
