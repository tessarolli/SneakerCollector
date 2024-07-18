// <copyright file="GetBrandsListQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Brands.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Queries.GetBrandsList;

/// <summary>
/// Mediator Handler for the <see cref="GetBrandsListQuery"/>.
/// </summary>
/// <param name="shoeRepository">Injected BrandRepository.</param>
public class GetBrandsListQueryHandler(IBrandRepository shoeRepository)
    : IQueryHandler<GetBrandsListQuery, List<BrandDto>>
{
    private readonly IBrandRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result<List<BrandDto>>> Handle(GetBrandsListQuery query, CancellationToken cancellationToken)
    {
        var result = new List<BrandDto>();

        var getAllBrandsResult = await _shoeRepository.GetAllAsync();
        if (getAllBrandsResult.IsFailed)
        {
            return Result.Fail(getAllBrandsResult.Errors);
        }

        foreach (var shoe in getAllBrandsResult.Value)
        {
            result.Add(new BrandDto(
                shoe.Id.Value,
                shoe.OwnerId,
                shoe.BrandId,
                shoe.Name,
                shoe.Price.Currency,
                shoe.Price.Amount,
                shoe.Size.Unit,
                shoe.Size.Value,
                shoe.Year,
                shoe.Rating,
                shoe.CreatedAtUtc));
        }

        return Result.Ok(result);
    }
}