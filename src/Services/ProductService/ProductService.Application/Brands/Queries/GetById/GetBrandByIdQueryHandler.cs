// <copyright file="GetBrandByIdQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Brands.Dtos;
using ProductService.Domain.Brands.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Queries.GetById;

/// <summary>
/// Mediator Handler for the <see cref="GetBrandByIdQuery"/>.
/// </summary>
/// <param name="brandRepository">Injected UserRepository.</param>
public class GetBrandByIdQueryHandler(IBrandRepository brandRepository)
    : IQueryHandler<GetBrandByIdQuery, BrandDto>
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    /// <inheritdoc/>
    public async Task<Result<BrandDto>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
    {
        var productDomainModel = await _brandRepository.GetByIdAsync(new BrandId(query.Id));
        if (productDomainModel.IsFailed)
        {
            return Result.Fail(productDomainModel.Errors);
        }

        return Result.Ok(new BrandDto(
            productDomainModel.Value.Id.Value,
            productDomainModel.Value.Name));
    }
}