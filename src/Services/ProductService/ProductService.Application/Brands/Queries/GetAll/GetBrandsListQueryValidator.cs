// <copyright file="GetBrandsListQueryValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;

namespace ProductService.Application.Brands.Queries.GetAll;

/// <summary>
/// Validator for the <see cref="GetBrandsListQuery"/>.
/// </summary>
public class GetBrandsListQueryValidator : AbstractValidator<GetBrandsListQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBrandsListQueryValidator"/> class.
    /// </summary>
    public GetBrandsListQueryValidator()
    {
    }
}