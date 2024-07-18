// <copyright file="GetShoesListQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Shoes.Queries.GetShoesList;

/// <summary>
/// Validator for the <see cref="GetShoesListQuery"/>.
/// </summary>
public class GetShoesListQueryValidator : AbstractValidator<GetShoesListQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetShoesListQueryValidator"/> class.
    /// </summary>
    public GetShoesListQueryValidator()
    {
    }
}
