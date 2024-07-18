// <copyright file="Price.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Domain.Shoes.Enums;
using SharedDefinitions.Domain.Common.DDD;

namespace ProductService.Domain.Shoes.ValueObjects;

/// <summary>
/// The Price value object.
/// </summary>
/// <param name="currency">Currency type.</param>
/// <param name="amount">Price Amount.</param>
public class Price(Currency currency, decimal amount) : ValueObject
{
    /// <summary>
    /// Gets the Price Amount.
    /// </summary>
    public decimal Amount { get; } = amount;

    /// <summary>
    /// Gets the Price Unit.
    /// </summary>
    public Currency Currency { get; } = currency;

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }
}