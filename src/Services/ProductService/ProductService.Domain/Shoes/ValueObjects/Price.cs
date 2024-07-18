// <copyright file="Price.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using ProductService.Domain.Shoes.Enums;
using SharedDefinitions.Domain.Common.DDD;

namespace ProductService.Domain.Shoes.ValueObjects;

/// <summary>
/// The Price value object.
/// </summary>
public class Price : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Price"/> class.
    /// </summary>
    /// <param name="currency">Currency type.</param>
    /// <param name="amount">Price Amount.</param>
    public Price(Currency currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }

    /// <summary>
    /// Gets the Price Amount.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Gets the Price Unit.
    /// </summary>
    public Currency Currency { get; }

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