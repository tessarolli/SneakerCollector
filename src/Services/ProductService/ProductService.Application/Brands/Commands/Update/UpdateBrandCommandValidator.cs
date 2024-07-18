// <copyright file="UpdateBrandCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Brands.Commands.UpdateBrand;

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
        RuleFor(shoe => shoe.BrandId)
            .NotNull()
            .GreaterThan(0);

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
