// <copyright file="AddShoeCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Abstractions.Services;
using ProductService.Application.Shoes.Dtos;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;
using SharedDefinitions.Application.Common.Errors;

namespace ProductService.Application.Shoes.Commands.AddShoe;

/// <summary>
/// Mediator Handler for the <see cref="AddShoeCommand"/>.
/// </summary>
/// <param name="brandRepository">Injected IBrandRepository.</param>
/// <param name="shoeRepository">Injected IShoeRepository.</param>
/// <param name="userService">Injected IUserService.</param>
public class AddShoeCommandHandler(
    IBrandRepository brandRepository,
    IShoeRepository shoeRepository,
    IUserService userService)
    : ICommandHandler<AddShoeCommand, ShoeDto>
{
    private readonly IBrandRepository _brandRepository = brandRepository;
    private readonly IShoeRepository _shoeRepository = shoeRepository;
    private readonly IUserService _userService = userService;

    /// <inheritdoc/>
    public async Task<Result<ShoeDto>> Handle(AddShoeCommand request, CancellationToken cancellationToken)
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

        var shoeDomainModelResult = Shoe.Create(
             null,
             request.OwnerId,
             request.Name,
             brandResult.Value,
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
            addResult.Value.Brand.Name,
            addResult.Value.Price.Currency,
            addResult.Value.Price.Amount,
            addResult.Value.Size.Unit,
            addResult.Value.Size.Value,
            addResult.Value.Year,
            addResult.Value.Rating,
            addResult.Value.CreatedAtUtc));
    }
}