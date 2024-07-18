// <copyright file="Shoe.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Domain.Dtos;
using ProductService.Domain.Shoes.Validators;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Domain.Common.DDD;

namespace ProductService.Domain.Shoes;

/// <summary>
/// Shoe Aggregate Root.
/// </summary>
public sealed class Shoe : AggregateRoot<ShoeId>
{
    private Shoe(ShoeId id)
        : base(id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the Shoe's Owner Id.
    /// </summary>
    public long OwnerId { get; init; }

    /// <summary>
    /// Gets the Shoe's Brand Id.
    /// </summary>
    public long BrandId { get; init; }

    /// <summary>
    /// Gets the Shoe's Name.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Gets the Shoe's Brand.
    /// </summary>
    public Brand Brand { get; init; } = null!;

    /// <summary>
    /// Gets the Shoe's Price.
    /// </summary>
    public Price Price { get; init; } = null!;

    /// <summary>
    /// Gets the Shoe's Size.
    /// </summary>
    public Size Size { get; init; } = null!;

    /// <summary>
    /// Gets the Shoe's Launch Year.
    /// </summary>
    public int Year { get; init; }

    /// <summary>
    /// Gets the Shoe's Rating.
    /// </summary>
    public int Rating { get; init; }

    /// <summary>
    /// Gets Shoe's Creation Date on UTC.
    /// </summary>
    public DateTime CreatedAtUtc { get; init; }

    /// <summary>
    /// Gets the Shoe's Owner <see cref="UserDto"/>.
    /// </summary>
    public Lazy<Task<UserDto?>>? Owner { get; init; } = null!;

    /// <summary>
    /// Shoe Creation Factory.
    /// </summary>
    /// <param name="id">The Shoe's Id.</param>
    /// <param name="ownerId">The Shoe's Owner User Id.</param>
    /// <param name="name">The Shoe's Name.</param>
    /// <param name="brand">The Shoe's Brand.</param>
    /// <param name="price">The Shoe's Base Price.</param>
    /// <param name="size">The Shoe's Size.</param>
    /// <param name="year">The Shoe's Launch Year.</param>
    /// <param name="rating">The Shoe's Rating.</param>
    /// <param name="createdAtUtc">The Shoe's Creation Date.</param>
    /// <param name="ownerLazyLoader">The Lazy Load Implementation for the fetching the Shoe's Owner <see cref="UserDto"/>.</param>
    /// <returns>Shoe Domain Instance.</returns>
    public static Result<Shoe> Create(
        ShoeId? id,
        long ownerId,
        string name,
        Brand brand,
        Price price,
        Size size,
        int year,
        int rating,
        DateTime? createdAtUtc = null,
        Lazy<Task<UserDto?>>? ownerLazyLoader = null)
    {
        var shoe = new Shoe(id ?? new ShoeId())
        {
            OwnerId = ownerId,
            BrandId = brand.Id.Value,
            Name = name,
            Brand = brand,
            Price = price,
            Size = size,
            Year = year,
            Rating = rating,
            CreatedAtUtc = createdAtUtc ?? DateTime.UtcNow,
            Owner = ownerLazyLoader,
        };

        var validationResult = shoe.Validate();
        if (!validationResult.IsSuccess)
        {
            return Result.Fail(validationResult.Errors);
        }

        return Result.Ok(shoe);
    }

    /// <inheritdoc/>
    protected override object GetValidator() => new ShoeValidator();
}