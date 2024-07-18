// <copyright file="ShoeId.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Domain.Common.DDD;

namespace ProductService.Domain.Shoes.ValueObjects;

/// <summary>
/// Shoe Id Value Object.
/// </summary>
/// <param name="id">Id value if exists.</param>
public sealed class ShoeId(long? id = null) : ValueObject
{
    /// <summary>
    /// Gets the Shoe ID.
    /// </summary>
    public long Value { get; } = id ?? 0;

    /// <summary>
    /// Method required for comparing value objects.
    /// </summary>
    /// <returns>An ienumerable with all the properties of the value object.</returns>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
