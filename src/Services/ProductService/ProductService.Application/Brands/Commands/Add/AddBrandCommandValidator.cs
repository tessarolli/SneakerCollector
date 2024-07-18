// <copyright file="AddBrandCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;
using ProductService.Domain.Brands;

namespace ProductService.Application.Brands.Commands.AddBrand;

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
        RuleFor(shoe => shoe.OwnerId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(shoe => shoe.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(shoe => shoe.Year)
            .GreaterThan(1900);

        RuleFor(shoe => shoe.BrandId)
            .NotNull();

        RuleFor(shoe => shoe.BrandName)
            .NotNull()
            .NotEmpty();

        RuleFor(shoe => shoe.Price)
            .NotNull()
            .GreaterThan(-1);

        RuleFor(shoe => shoe.Currency)
            .NotEmpty();

        RuleFor(shoe => shoe.Size)
            .NotNull()
            .GreaterThan(-1);

        RuleFor(shoe => shoe.SizeUnit)
            .NotEmpty();
    }
}
