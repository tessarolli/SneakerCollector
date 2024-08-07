﻿// <copyright file="UpdateShoeRequest.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Contracts.Shoes.Requests;

/// <summary>
/// A request to update the Shoe in the repository.
/// </summary>
/// <param name="ShoeId">The Shoe's Id.</param>
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
public record UpdateShoeRequest(
    long ShoeId,
    long OwnerId,
    long BrandId,
    string BrandName,
    string Name,
    int Currency,
    decimal Price,
    int SizeUnit,
    decimal Size,
    int Year,
    int Rating);