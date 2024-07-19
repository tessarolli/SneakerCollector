// <copyright file="GetBrandByIdQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Brands.Queries.GetById;

/// <summary>
/// Validator for the <see cref="GetBrandByIdQuery"/>.
/// </summary>
public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBrandByIdQueryValidator"/> class.
    /// </summary>
    public GetBrandByIdQueryValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty()
              .WithMessage("Brand Id cannot be empty")
          .GreaterThan(0)
              .WithMessage("Brand Id have to be greater than zero");
    }
}
