// <copyright file="DeleteProductCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using FluentResults;
using SneakerCollector.Services.ProductService.Application.Abstractions.Repositories;
using SneakerCollector.Services.ProductService.Domain.Products.ValueObjects;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.ProductService.Application.Products.Commands.DeleteProduct;

/// <summary>
/// Mediator Handler for the <see cref="DeleteProductCommand"/>.
/// </summary>
public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductCommandHandler"/> class.
    /// </summary>
    /// <param name="ProductRepository">Injected UserRepository.</param>
    public DeleteProductCommandHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _productRepository.RemoveAsync(new ProductId(request.Id));
    }
}
