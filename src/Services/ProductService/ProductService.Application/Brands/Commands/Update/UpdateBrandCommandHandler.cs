// <copyright file="UpdateBrandCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Brands.Dtos;
using ProductService.Domain.Brands;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.Update;

/// <summary>
/// Mediator Handler for the <see cref="UpdateBrandCommand"/>.
/// </summary>
/// <param name="brandRepository">Injected UserRepository.</param>
public class UpdateBrandCommandHandler(IBrandRepository brandRepository) : ICommandHandler<UpdateBrandCommand, BrandDto>
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    /// <inheritdoc/>
    public async Task<Result<BrandDto>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brandDomainModelResult = Brand.Create(new(request.BrandId), request.BrandName);
        if (brandDomainModelResult.IsFailed)
        {
            return Result.Fail(brandDomainModelResult.Errors);
        }

        var updateResult = await _brandRepository.UpdateAsync(brandDomainModelResult.Value);
        if (!updateResult.IsSuccess)
        {
            return Result.Fail(updateResult.Errors);
        }

        return Result.Ok(new BrandDto(
            updateResult.Value.Id.Value,
            updateResult.Value.Name));
    }
}