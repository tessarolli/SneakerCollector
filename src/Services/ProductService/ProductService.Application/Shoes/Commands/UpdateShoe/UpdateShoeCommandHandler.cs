// <copyright file="UpdateShoeCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Abstractions.Services;
using ProductService.Application.Shoes.Dtos;
using ProductService.Domain.Brands;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Application.Common.Errors;

namespace ProductService.Application.Shoes.Commands.UpdateShoe;

/// <summary>
/// Mediator Handler for the <see cref="UpdateShoeCommand"/>.
/// </summary>
/// <param name="brandRepository">Injected IBrandRepository.</param>
/// <param name="shoeRepository">Injected IShoeRepository.</param>
/// <param name="userService">Injected IUserService.</param>
public class UpdateShoeCommandHandler(
    IBrandRepository brandRepository,
    IShoeRepository shoeRepository,
    IUserService userService)
    : ICommandHandler<UpdateShoeCommand, ShoeDto>
{
    private readonly IBrandRepository _brandRepository = brandRepository;
    private readonly IShoeRepository _shoeRepository = shoeRepository;
    private readonly IUserService _userService = userService;

    /// <inheritdoc/>
    public async Task<Result<ShoeDto>> Handle(UpdateShoeCommand request, CancellationToken cancellationToken)
    {
        var brandResult = await _brandRepository.GetByIdAsync(new(request.BrandId));
        if (brandResult.IsFailed)
        {
            return Result.Fail(brandResult.Errors);
        }

        var shoeOwner = await _userService.GetUserByIdAsync(request.OwnerId);
        if (shoeOwner is null)
        {
            return Result.Fail(new NotFoundError("Could not find the specified user."));
        }

        var shoeDomainModel = Shoe.Create(
            new ShoeId(request.ShoeId),
            request.OwnerId,
            request.Name,
            brandResult.Value,
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