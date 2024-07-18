// <copyright file="DeleteBrandCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Brands.Commands.DeleteBrand;

/// <summary>
/// Command to remove a product from the catalog.
/// </summary>
/// <param name="Id">The Id of the product being deleted.</param>
public record DeleteBrandCommand(long Id) : ICommand;