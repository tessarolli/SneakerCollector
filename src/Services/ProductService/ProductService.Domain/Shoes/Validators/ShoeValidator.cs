// <copyright file="ShoeValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Domain.Common;

namespace ProductService.Domain.Shoes.Validators;

/// <summary>
/// Shoe Entity Validation Rules.
/// </summary>
public sealed class ShoeValidator : EntityValidator<ShoeId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShoeValidator"/> class.
    /// </summary>
    public ShoeValidator()
    {
        RuleFor(g => ((Shoe)g).Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(g => ((Shoe)g).Year)
            .GreaterThan(1900);

        RuleFor(g => ((Shoe)g).Brand)
            .NotNull();

        RuleFor(g => ((Shoe)g).Price)
            .NotNull();

        RuleFor(g => ((Shoe)g).Size)
            .NotNull();
    }
}