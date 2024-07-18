// <copyright file="GetShoesListQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Shoes.Dtos;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Queries.GetShoesList;

/// <summary>
/// Mediator Handler for the <see cref="GetProductsListQuery"/>.
/// </summary>
/// <param name="shoeRepository">Injected ShoeRepository.</param>
public class GetShoesListQueryHandler(IShoeRepository shoeRepository)
    : IQueryHandler<GetProductsListQuery, List<ShoeDto>>
{
    private readonly IShoeRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result<List<ShoeDto>>> Handle(GetProductsListQuery query, CancellationToken cancellationToken)
    {
        var result = new List<ShoeDto>();

        var getAllShoesResult = await _shoeRepository.GetAllAsync();
        if (getAllShoesResult.IsFailed)
        {
            return Result.Fail(getAllShoesResult.Errors);
        }

        foreach (var shoe in getAllShoesResult.Value)
        {
            result.Add(new ShoeDto(
                shoe.Id.Value,
                shoe.OwnerId,
                shoe.BrandId,
                shoe.Name,
                shoe.Price.Currency,
                shoe.Price.Amount,
                shoe.Size.Unit,
                shoe.Size.Value,
                shoe.CreatedAtUtc));
        }

        return Result.Ok(result);
    }
}