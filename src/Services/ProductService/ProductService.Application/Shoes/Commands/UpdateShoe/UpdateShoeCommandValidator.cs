// <copyright file="UpdateShoeCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Shoes.Commands.UpdateShoe;

/// <summary>
/// Validator for the <see cref="UpdateShoeCommand"/>.
/// </summary>
public class UpdateShoeCommandValidator : AbstractValidator<UpdateShoeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateShoeCommandValidator"/> class.
    /// </summary>
    public UpdateShoeCommandValidator()
    {
        RuleFor(shoe => shoe.ShoeId)
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
