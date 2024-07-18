// <copyright file="GetBrandByIdQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Brands.Dtos;
using ProductService.Domain.Brands.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Queries.GetBrandById;

/// <summary>
/// Mediator Handler for the <see cref="GetBrandByIdQuery"/>.
/// </summary>
/// <param name="shoeRepository">Injected UserRepository.</param>
public class GetBrandByIdQueryHandler(IBrandRepository shoeRepository)
    : IQueryHandler<GetBrandByIdQuery, BrandDto>
{
    private readonly IBrandRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result<BrandDto>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
    {
        var productDomainModel = await _shoeRepository.GetByIdAsync(new BrandId(query.Id));
        if (productDomainModel.IsFailed)
        {
            return Result.Fail(productDomainModel.Errors);
        }

        return Result.Ok(new BrandDto(
            productDomainModel.Value.Id.Value,
            productDomainModel.Value.OwnerId,
            productDomainModel.Value.BrandId,
            productDomainModel.Value.Name,
            productDomainModel.Value.Price.Currency,
            productDomainModel.Value.Price.Amount,
            productDomainModel.Value.Size.Unit,
            productDomainModel.Value.Size.Value,
            productDomainModel.Value.Year,
            productDomainModel.Value.Rating,
            productDomainModel.Value.CreatedAtUtc));
    }
}