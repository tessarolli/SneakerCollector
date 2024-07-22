// <copyright file="GetBrandsListQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Brands.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Application.Models;

namespace ProductService.Application.Brands.Queries.GetAll;

/// <summary>
/// Mediator Handler for the <see cref="GetBrandsListQuery"/>.
/// </summary>
/// <param name="brandRepository">Injected BrandRepository.</param>
public class GetBrandsListQueryHandler(IBrandRepository brandRepository)
    : IQueryHandler<GetBrandsListQuery, PagedResult<BrandDto>>
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    /// <inheritdoc/>
    public async Task<Result<PagedResult<BrandDto>>> Handle(GetBrandsListQuery query, CancellationToken cancellationToken)
    {
        var result = new List<BrandDto>();

        var getAllBrandsResult = await _brandRepository.GetAllAsync(query.Request);
        if (getAllBrandsResult.IsFailed)
        {
            return Result.Fail(getAllBrandsResult.Errors);
        }

        foreach (var brand in getAllBrandsResult.Value.Items)
        {
            result.Add(new BrandDto(
                brand.Id.Value,
                brand.Name));
        }

        return Result.Ok(new PagedResult<BrandDto>
        {
            Items = result,
            TotalCount = getAllBrandsResult.Value.TotalCount,
            Offset = getAllBrandsResult.Value.Offset,
            Limit = getAllBrandsResult.Value.Limit,
        });
    }
}