// <copyright file="GetProductsListQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Shoes.Queries.GetShoesList;

/// <summary>
/// Validator for the <see cref="GetProductsListQuery"/>.
/// </summary>
public class GetProductsListQueryValidator : AbstractValidator<GetProductsListQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductsListQueryValidator"/> class.
    /// </summary>
    public GetProductsListQueryValidator()
    {
    }
}
