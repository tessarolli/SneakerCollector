// <copyright file="DeleteBrandCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Brands.Commands.Delete;

/// <summary>
/// Validator for the <see cref="DeleteBrandCommand"/>.
/// </summary>
public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBrandCommandValidator"/> class.
    /// </summary>
    public DeleteBrandCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("Brand Id cannot be empty")
            .GreaterThan(0)
                .WithMessage("Brand Id have to be greater than zero");
    }
}
