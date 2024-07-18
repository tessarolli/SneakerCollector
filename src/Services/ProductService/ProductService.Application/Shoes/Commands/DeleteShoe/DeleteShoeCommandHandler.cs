// <copyright file="DeleteShoeCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Commands.DeleteShoe;

/// <summary>
/// Mediator Handler for the <see cref="DeleteShoeCommand"/>.
/// </summary>
/// <param name="shoeRepository">Injected UserRepository.</param>
public class DeleteShoeCommandHandler(IShoeRepository shoeRepository) : ICommandHandler<DeleteShoeCommand>
{
    private readonly IShoeRepository _shoeRepository = shoeRepository;

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteShoeCommand request, CancellationToken cancellationToken)
    {
        return await _shoeRepository.RemoveAsync(new ShoeId(request.Id));
    }
}