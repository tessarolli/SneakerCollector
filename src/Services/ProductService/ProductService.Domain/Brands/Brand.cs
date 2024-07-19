// <copyright file="Brand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Domain.Brands.Validators;
using ProductService.Domain.Brands.ValueObjects;
using SharedDefinitions.Domain.Common.DDD;

namespace ProductService.Domain.Brands;

/// <summary>
/// Brand Entity.
/// </summary>
public class Brand : Entity<BrandId>
{
    private Brand(BrandId id)
       : base(id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the Brand's Name.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Brand Creation Factory.
    /// </summary>
    /// <param name="id">The Brand's Id.</param>
    /// <param name="name">The Brand's Name.</param>
    /// <returns>Brand Domain Instance.</returns>
    public static Result<Brand> Create(
        BrandId? id,
        string name)
    {
        var brand = new Brand(id ?? new BrandId())
        {
            Name = name,
        };

        var validationResult = brand.Validate();
        if (!validationResult.IsSuccess)
        {
            return Result.Fail(validationResult.Errors);
        }

        return Result.Ok(brand);
    }

    /// <inheritdoc/>
    protected override object GetValidator() => new BrandValidator();
}