// <copyright file="DeleteBrandCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Brands.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.DeleteBrand;

/// <summary>
/// Mediator Handler for the <see cref="DeleteBrandCommand"/>.
/// </summary>
/// <param name="shoeRepository">Injected UserRepository.</param>
public class DeleteBrandCommandHandler(IBrandRepository shoeRepository) : ICommandHandler<DeleteBrandCommand>
{
    private readonly IBrandRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        return await _shoeRepository.RemoveAsync(new BrandId(request.Id));
    }
}