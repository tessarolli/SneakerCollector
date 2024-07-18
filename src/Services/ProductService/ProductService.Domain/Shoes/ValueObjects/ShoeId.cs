// <copyright file="ShoeId.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Domain.Common.DDD;

namespace ProductService.Domain.Shoes.ValueObjects;

/// <summary>
/// Shoe Id Value Object.
/// </summary>
public sealed class ShoeId : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShoeId"/> class.
    /// </summary>
    /// <param name="id">Id value if exists.</param>
    public ShoeId(long? id = null)
    {
        Value = id ?? 0;
    }

    /// <summary>
    /// Gets the Shoe ID.
    /// </summary>
    public long Value { get; }

    /// <summary>
    /// Method required for comparing value objects.
    /// </summary>
    /// <returns>An ienumerable with all the properties of the value object.</returns>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
