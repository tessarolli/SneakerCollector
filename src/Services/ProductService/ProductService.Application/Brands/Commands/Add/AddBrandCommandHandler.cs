// <copyright file="AddBrandCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Brands.Dtos;
using ProductService.Domain.Brands;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.AddBrand;

/// <summary>
/// Mediator Handler for the <see cref="AddBrandCommand"/>.
/// </summary>
/// <param name="shoeRepository">Injected BrandRepository.</param>
public class AddBrandCommandHandler(IBrandRepository shoeRepository) : ICommandHandler<AddBrandCommand, BrandDto>
{
    private readonly IBrandRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result<BrandDto>> Handle(AddBrandCommand request, CancellationToken cancellationToken)
    {
        var brandDomainModelResult = Brand.Create(new(request.BrandId), request.BrandName);
        if (brandDomainModelResult.IsFailed)
        {
            return Result.Fail(brandDomainModelResult.Errors);
        }

        var shoeDomainModelResult = Brand.Create(
             null,
             request.OwnerId,
             request.Name,
             brandDomainModelResult.Value,
             new Price(request.Currency, request.Price),
             new Size(request.SizeUnit, request.Size),
             request.Year,
             request.Rating,
             DateTime.UtcNow);

        if (!shoeDomainModelResult.IsSuccess)
        {
            return Result.Fail(shoeDomainModelResult.Errors);
        }

        var addResult = await _shoeRepository.AddAsync(shoeDomainModelResult.Value);
        if (!addResult.IsSuccess)
        {
            return Result.Fail(addResult.Errors);
        }

        return Result.Ok(new BrandDto(
            addResult.Value.Id.Value,
            addResult.Value.OwnerId,
            addResult.Value.BrandId,
            addResult.Value.Name,
            addResult.Value.Price.Currency,
            addResult.Value.Price.Amount,
            addResult.Value.Size.Unit,
            addResult.Value.Size.Value,
            addResult.Value.Year,
            addResult.Value.Rating,
            addResult.Value.CreatedAtUtc));
    }
}