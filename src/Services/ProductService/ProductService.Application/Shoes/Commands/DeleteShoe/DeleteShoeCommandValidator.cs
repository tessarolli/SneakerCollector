// <copyright file="DeleteShoeCommandValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Shoes.Commands.DeleteShoe;

/// <summary>
/// Validator for the <see cref="DeleteShoeCommand"/>.
/// </summary>
public class DeleteShoeCommandValidator : AbstractValidator<DeleteShoeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteShoeCommandValidator"/> class.
    /// </summary>
    public DeleteShoeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("Shoe Id cannot be empty")
            .GreaterThan(0)
                .WithMessage("Shoe Id have to be greater than zero");
    }
}
