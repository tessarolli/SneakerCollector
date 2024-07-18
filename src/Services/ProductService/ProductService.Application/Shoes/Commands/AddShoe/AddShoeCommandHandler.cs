// <copyright file="AddShoeCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Shoes.Dtos;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Commands.AddShoe;

/// <summary>
/// Mediator Handler for the <see cref="AddShoeCommand"/>.
/// </summary>
/// <param name="shoeRepository">Injected ShoeRepository.</param>
public class AddShoeCommandHandler(IShoeRepository shoeRepository) : ICommandHandler<AddShoeCommand, ShoeDto>
{
    private readonly IShoeRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result<ShoeDto>> Handle(AddShoeCommand request, CancellationToken cancellationToken)
    {
        var brandDomainModelResult = Brand.Create(new(request.BrandId), request.BrandName);
        if (brandDomainModelResult.IsFailed)
        {
            return Result.Fail(brandDomainModelResult.Errors);
        }

        var shoeDomainModelResult = Shoe.Create(
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

        return Result.Ok(new ShoeDto(
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