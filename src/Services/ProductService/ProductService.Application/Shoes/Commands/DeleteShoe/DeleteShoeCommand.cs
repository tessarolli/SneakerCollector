// <copyright file="DeleteShoeCommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Application.Abstractions.Messaging;

namespace ProductService.Application.Shoes.Commands.DeleteShoe;

/// <summary>
/// Command to remove a product from the catalog.
/// </summary>
/// <param name="Id">The Id of the product being deleted.</param>
public record DeleteShoeCommand(long Id) : ICommand;