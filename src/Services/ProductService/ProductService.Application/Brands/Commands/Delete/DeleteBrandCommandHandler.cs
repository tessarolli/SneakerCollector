// <copyright file="DeleteBrandCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Brands.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.Delete;

/// <summary>
/// Mediator Handler for the <see cref="DeleteBrandCommand"/>.
/// </summary>
/// <param name="brandRepository">Injected UserRepository.</param>
public class DeleteBrandCommandHandler(IBrandRepository brandRepository) : ICommandHandler<DeleteBrandCommand>
{
    private readonly IBrandRepository _brandRepository = brandRepository;

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        return await _brandRepository.RemoveAsync(new BrandId(request.Id));
    }
}