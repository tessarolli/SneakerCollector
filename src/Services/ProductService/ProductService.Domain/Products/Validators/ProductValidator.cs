// <copyright file="ProductValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using FluentValidation;
using SneakerCollector.Services.ProductService.Domain.Products.ValueObjects;
using SneakerCollector.SharedDefinitions.Domain.Common;

namespace SneakerCollector.Services.ProductService.Domain.Products.Validators;

/// <summary>
/// Product Entity Validation Rules.
/// </summary>
public sealed class ProductValidator : EntityValidator<Product, ProductId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductValidator"/> class.
    /// </summary>
    public ProductValidator()
    {
        RuleFor(g => ((Product)g).Name)
            .NotEmpty()
            .WithMessage("The Product name is required.");

        RuleFor(g => ((Product)g).Name)
            .MaximumLength(255)
            .WithMessage("The Product name must be at most 255 characters long.");
    }
}
