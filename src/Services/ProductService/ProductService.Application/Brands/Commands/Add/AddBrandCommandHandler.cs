// <copyright file="AddBrandCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Brands.Dtos;
using ProductService.Domain.Brands;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.Add;

/// <summary>
/// Mediator Handler for the <see cref="AddBrandCommand"/>.
/// </summary>
/// <param name="brandRepository">Injected BrandRepository.</param>
public class AddBrandCommandHandler(IBrandRepository brandRepository) : ICommandHandler<AddBrandCommand, BrandDto>
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    /// <inheritdoc/>
    public async Task<Result<BrandDto>> Handle(AddBrandCommand request, CancellationToken cancellationToken)
    {
        var brandDomainModelResult = Brand.Create(new(request.BrandId), request.BrandName);
        if (brandDomainModelResult.IsFailed)
        {
            return Result.Fail(brandDomainModelResult.Errors);
        }

        var addResult = await _brandRepository.AddAsync(brandDomainModelResult.Value);
        if (!addResult.IsSuccess)
        {
            return Result.Fail(addResult.Errors);
        }

        return Result.Ok(new BrandDto(
            addResult.Value.Id.Value,
            addResult.Value.Name));
    }
}