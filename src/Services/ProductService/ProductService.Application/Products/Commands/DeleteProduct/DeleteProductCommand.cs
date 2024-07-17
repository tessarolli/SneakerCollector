// <copyright file="DeleteProductCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>


using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.ProductService.Application.Products.Commands.DeleteProduct;

/// <summary>
/// Command to remove a product from the catalog.
/// </summary>
/// <param name="Id">The Id of the product being deleted.</param>
public record DeleteProductCommand(long Id) : ICommand;
