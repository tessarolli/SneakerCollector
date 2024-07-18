// <copyright file="GetShoeByIdQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Shoes.Dtos;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Queries.GetShoeById;

/// <summary>
/// Mediator Handler for the <see cref="GetShoeByIdQuery"/>.
/// </summary>
/// <param name="shoeRepository">Injected UserRepository.</param>
public class GetShoeByIdQueryHandler(IShoeRepository shoeRepository)
    : IQueryHandler<GetShoeByIdQuery, ShoeDto>
{
    private readonly IShoeRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result<ShoeDto>> Handle(GetShoeByIdQuery query, CancellationToken cancellationToken)
    {
        var productDomainModel = await _shoeRepository.GetByIdAsync(new ShoeId(query.Id));
        if (productDomainModel.IsFailed)
        {
            return Result.Fail(productDomainModel.Errors);
        }

        return Result.Ok(new ShoeDto(
            productDomainModel.Value.Id.Value,
            productDomainModel.Value.OwnerId,
            productDomainModel.Value.BrandId,
            productDomainModel.Value.Name,
            productDomainModel.Value.Price.Currency,
            productDomainModel.Value.Price.Amount,
            productDomainModel.Value.Size.Unit,
            productDomainModel.Value.Size.Value,
            productDomainModel.Value.CreatedAtUtc));
    }
}