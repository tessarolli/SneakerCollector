// <copyright file="GetProductsListQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using FluentValidation;

namespace SneakerCollector.Services.ProductService.Application.Products.Queries.GetProductsList;

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
