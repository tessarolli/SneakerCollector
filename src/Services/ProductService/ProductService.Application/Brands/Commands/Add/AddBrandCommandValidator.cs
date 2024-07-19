// <copyright file="AddBrandCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Brands.Commands.Add;

/// <summary>
/// Validator for the <see cref="AddBrandCommand"/>.
/// </summary>
public class AddBrandCommandValidator : AbstractValidator<AddBrandCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddBrandCommandValidator"/> class.
    /// </summary>
    public AddBrandCommandValidator()
    {
        RuleFor(brand => brand.BrandName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);
    }
}