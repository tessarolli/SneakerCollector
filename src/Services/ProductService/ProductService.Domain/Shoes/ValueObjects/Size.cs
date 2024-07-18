// <copyright file="Size.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Domain.Shoes.Enums;
using SharedDefinitions.Domain.Common.DDD;

namespace ProductService.Domain.Shoes.ValueObjects;

/// <summary>
/// The Size value object.
/// </summary>
/// <param name="unit">The Unit Type.</param>
/// <param name="value">Size amount.</param>
public class Size(ShoeSizeUnit unit, decimal value) : ValueObject
{
    /// <summary>
    /// Gets the Shoe Size value.
    /// </summary>
    public decimal Value { get; } = value;

    /// <summary>
    /// Gets the Shoe Size Unit.
    /// </summary>
    public ShoeSizeUnit Unit { get; } = unit;

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Unit;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Value} {Unit}";
    }
}