// <copyright file="UpdateShoeCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Shoes.Dtos;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Commands.UpdateShoe;

/// <summary>
/// Mediator Handler for the <see cref="UpdateShoeCommand"/>.
/// </summary>
/// <param name="shoeRepository">Injected UserRepository.</param>
public class UpdateShoeCommandHandler(IShoeRepository shoeRepository) : ICommandHandler<UpdateShoeCommand, ShoeDto>
{
    private readonly IShoeRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result<ShoeDto>> Handle(UpdateShoeCommand request, CancellationToken cancellationToken)
    {
        var brandDomainModelResult = Brand.Create(new(request.BrandId), request.BrandName);
        if (brandDomainModelResult.IsFailed)
        {
            return Result.Fail(brandDomainModelResult.Errors);
        }

        var shoeDomainModel = Shoe.Create(
            new ShoeId(request.ShoeId),
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

        return Result.Ok(new ShoeDto(
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