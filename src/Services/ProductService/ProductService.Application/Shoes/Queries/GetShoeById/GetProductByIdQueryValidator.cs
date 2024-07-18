// <copyright file="GetProductByIdQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Shoes.Queries.GetShoeById;

/// <summary>
/// Validator for the <see cref="GetShoeByIdQuery"/>.
/// </summary>
public class GetProductByIdQueryValidator : AbstractValidator<GetShoeByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductByIdQueryValidator"/> class.
    /// </summary>
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty()
              .WithMessage("Shoe Id cannot be empty")
          .GreaterThan(0)
              .WithMessage("Shoe Id have to be greater than zero");
    }
}
