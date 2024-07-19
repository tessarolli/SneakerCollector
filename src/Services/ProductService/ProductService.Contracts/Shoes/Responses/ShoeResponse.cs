// <copyright file="ShoeResponse.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Shoes.Responses;

/// <summary>
/// The Contract for Shoe Response.
/// Contract for a Shoe Aggregate Instance Response.
/// Can be used in collections.
/// </summary>
/// <param name="Id">The Shoe's Id.</param>
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
/// <param name="CreatedAtUtc">The Shoe's Creation Date in UTC.</param>
public record ShoeResponse(
    long Id,
    long OwnerId,
    long BrandId,
    string BrandName,
    string Name,
    int Currency,
    decimal Price,
    int SizeUnit,
    decimal Size,
    int Year,
    int Rating,
    DateTime CreatedAtUtc);