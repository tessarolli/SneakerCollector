// <copyright file="ShoeDb.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace ProductService.Infrastructure.Dtos;

/// <summary>
/// Shoe model as a representation of the database schema.
/// </summary>
public record ShoeDb(
    int id,
    long owner_id,
    long brand_id,
    string name,
    decimal size_value,
    int size_unit,
    decimal price_amount,
    int price_currency,
    int launch_year,
    int rating,
    DateTime created_at_utc);