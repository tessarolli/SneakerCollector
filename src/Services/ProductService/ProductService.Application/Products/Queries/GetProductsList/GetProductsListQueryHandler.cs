// <copyright file="GetProductsListQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;
using SneakerCollector.Services.ProductService.Application.Abstractions.Repositories;
using SneakerCollector.Services.ProductService.Application.Products.Dtos;
using FluentResults;
using SneakerCollector.SharedDefinitions.Application.Common.Errors;

namespace SneakerCollector.Services.ProductService.Application.Products.Queries.GetProductsList;

/// <summary>
/// Mediator Handler for the <see cref="GetProductsListQuery"/>.
/// </summary>
public class GetProductsListQueryHandler : IQueryHandler<GetProductsListQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductsListQueryHandler"/> class.
    /// </summary>
    /// <param name="ProductRepository">Injected UserRepository.</param>
    public GetProductsListQueryHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<List<ProductDto>>> Handle(GetProductsListQuery query, CancellationToken cancellationToken)
    {
        var result = new List<ProductDto>();

        var getAllProductsResult = await _productRepository.GetAllAsync();
        if (!getAllProductsResult.IsSuccess)
        {
            return Result.Fail(getAllProductsResult.Errors);
        }

        foreach (var product in getAllProductsResult.Value)
        {
            var discountTask = product.Discount?.Value;
            if (discountTask is null)
            {
                return Result.Fail(new UnreachableExternalServiceError("Discount Service"));
            }

            result.Add(new ProductDto(
                product.Id.Value,
                product.OwnerId,
                product.Name,
                product.Description,
                product.StatusName,
                product.Stock,
                product.Price,
                (await discountTask)?.Amount ?? 0,
                await product.FinalPrice(),
                product.CreatedAtUtc));
        }

        return Result.Ok(result);
    }
}