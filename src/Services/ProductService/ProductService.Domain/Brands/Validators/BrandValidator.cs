// <copyright file="BrandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;
using ProductService.Domain.Brands.ValueObjects;
using SharedDefinitions.Domain.Common;

namespace ProductService.Domain.Brands.Validators;

/// <summary>
/// Brand Entity Validation Rules.
/// </summary>
public sealed class BrandValidator : EntityValidator<BrandId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrandValidator"/> class.
    /// </summary>
    public BrandValidator()
    {
        RuleFor(g => ((Brand)g).Name)
            .NotNull()
            .NotEmpty();
    }
}