// <copyright file="UpdateBrandCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Brands.Commands.Update;

/// <summary>
/// Validator for the <see cref="UpdateBrandCommand"/>.
/// </summary>
public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBrandCommandValidator"/> class.
    /// </summary>
    public UpdateBrandCommandValidator()
    {
        RuleFor(brand => brand.BrandId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(brand => brand.BrandName)
            .NotNull()
            .NotEmpty();
    }
}