// <copyright file="ShoesController.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Contracts.Enums;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Shoes.Commands.AddShoe;
using ProductService.Application.Shoes.Commands.DeleteShoe;
using ProductService.Application.Shoes.Commands.UpdateShoe;
using ProductService.Application.Shoes.Dtos;
using ProductService.Application.Shoes.Queries.GetShoeById;
using ProductService.Application.Shoes.Queries.GetShoesList;
using ProductService.Contracts.Shoes.Requests;
using ProductService.Contracts.Shoes.Responses;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Presentation.Attributes;
using SharedDefinitions.Presentation.Controllers;

namespace ProductService.API.Controllers;

/// <summary>
/// ShoesController Controller.
/// </summary>
/// <param name="mediator">Injected _mediator.</param>
/// <param name="mapper">Injected _mapper.</param>
/// <param name="logger">Injected Logger.</param>
/// <param name="exceptionHandlingService">Injected _exceptionHandlingService.</param>
[Route("[controller]")]
public class ShoesController(
    IMediator mediator,
    IMapper mapper,
    ILogger<ShoesController> logger,
    IExceptionHandlingService exceptionHandlingService)
    : ResultControllerBase<ShoesController>(mediator, mapper, logger, exceptionHandlingService)
{
    /// <summary>
    /// Gets a list of Shoes.
    /// </summary>
    /// <returns>The list of Shoes.</returns>
    [HttpGet]
    [RoleAuthorize]
    public async Task<IActionResult> GetAll() =>
        await HandleRequestAsync<GetShoesListQuery, List<ShoeDto>, List<ProductResponse>>();

    /// <summary>
    /// Gets a Shoe by its Id.
    /// </summary>
    /// <param name="id">Shoe Id.</param>
    /// <returns>The Shoe Aggregate.</returns>
    [HttpGet("{id:long}")]
    [RoleAuthorize]
    public async Task<IActionResult> GetById(long id) =>
        await HandleRequestAsync<GetShoeByIdQuery, ShoeDto, ProductResponse>(id);

    /// <summary>
    /// Add a Shoe to the Shoe Repository.
    /// </summary>
    /// <param name="request">Shoe data.</param>
    /// <returns>The Shoe instance created with Id.</returns>
    [HttpPost]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> Add(AddShoeRequest request) =>
        await HandleRequestAsync<AddShoeCommand, ShoeDto, ProductResponse>(request);

    /// <summary>
    /// Updates a Shoe in the Shoe Repository.
    /// </summary>
    /// <param name="request">Shoe data.</param>
    /// <returns>The Shoe instance created with Id.</returns>
    [HttpPut]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> Update(UpdateShoeRequest request) =>
        await HandleRequestAsync<UpdateShoeCommand, ShoeDto, ProductResponse>(request);

    /// <summary>
    /// Deletes a Shoe from the Shoe Repository.
    /// </summary>
    /// <param name="id">Shoe Id.</param>
    /// <returns>The Action Result of the delete operation.</returns>
    [HttpDelete("{id:long}")]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> Delete(long id) =>
        await HandleRequestAsync<DeleteShoeCommand, Result, object>(id);
}