// <copyright file="UpdateBrandCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Brands.Dtos;
using ProductService.Domain.Brands;
using ProductService.Domain.Brands.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.UpdateBrand;

/// <summary>
/// Mediator Handler for the <see cref="UpdateBrandCommand"/>.
/// </summary>
/// <param name="shoeRepository">Injected UserRepository.</param>
public class UpdateBrandCommandHandler(IBrandRepository shoeRepository) : ICommandHandler<UpdateBrandCommand, BrandDto>
{
    private readonly IBrandRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result<BrandDto>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandDomainModelResult = Brand.Create(new(request.BrandId), request.BrandName);
        if (brandDomainModelResult.IsFailed)
        {
            return Result.Fail(brandDomainModelResult.Errors);
        }

        var shoeDomainModel = Brand.Create(
            new BrandId(request.BrandId),
            request.OwnerId,
            request.Name,
            brandDomainModelResult.Value,
            new Price(request.Currency, request.Price),
            new Size(request.SizeUnit, request.Size),
            request.Year,
            request.Rating);

        if (!shoeDomainModel.IsSuccess)
        {
            return Result.Fail(shoeDomainModel.Errors);
        }

        var updateResult = await _shoeRepository.UpdateAsync(shoeDomainModel.Value);
        if (!updateResult.IsSuccess)
        {
            return Result.Fail(updateResult.Errors);
        }

        return Result.Ok(new BrandDto(
            updateResult.Value.Id.Value,
            updateResult.Value.OwnerId,
            updateResult.Value.BrandId,
            updateResult.Value.Name,
            updateResult.Value.Price.Currency,
            updateResult.Value.Price.Amount,
            updateResult.Value.Size.Unit,
            updateResult.Value.Size.Value,
            updateResult.Value.Year,
            updateResult.Value.Rating,
            updateResult.Value.CreatedAtUtc));
    }
}