// <copyright file="AddShoeCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;
using ProductService.Domain.Shoes;

namespace ProductService.Application.Shoes.Commands.AddShoe;

/// <summary>
/// Validator for the <see cref="AddShoeCommand"/>.
/// </summary>
public class AddShoeCommandValidator : AbstractValidator<AddShoeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddShoeCommandValidator"/> class.
    /// </summary>
    public AddShoeCommandValidator()
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
