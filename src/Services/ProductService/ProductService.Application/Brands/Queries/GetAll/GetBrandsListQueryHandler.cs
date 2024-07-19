// <copyright file="GetBrandsListQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Brands.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Queries.GetAll;

/// <summary>
/// Mediator Handler for the <see cref="GetBrandsListQuery"/>.
/// </summary>
/// <param name="brandRepository">Injected BrandRepository.</param>
public class GetBrandsListQueryHandler(IBrandRepository brandRepository)
    : IQueryHandler<GetBrandsListQuery, List<BrandDto>>
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    /// <inheritdoc/>
    public async Task<Result<List<BrandDto>>> Handle(GetBrandsListQuery query, CancellationToken cancellationToken)
    {
        var result = new List<BrandDto>();

        var getAllBrandsResult = await _brandRepository.GetAllAsync();
        if (getAllBrandsResult.IsFailed)
        {
            return Result.Fail(getAllBrandsResult.Errors);
        }

        foreach (var brand in getAllBrandsResult.Value)
        {
            result.Add(new BrandDto(
                brand.Id.Value,
                brand.Name));
        }

        return Result.Ok(result);
    }
}