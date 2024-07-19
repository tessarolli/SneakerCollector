// <copyright file="BrandsController.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Contracts.Enums;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Brands.Commands.Add;
using ProductService.Application.Brands.Commands.Delete;
using ProductService.Application.Brands.Commands.Update;
using ProductService.Application.Brands.Dtos;
using ProductService.Application.Brands.Queries.GetAll;
using ProductService.Application.Brands.Queries.GetById;
using ProductService.Contracts.Brands.Requests;
using ProductService.Contracts.Brands.Responses;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Presentation.Attributes;
using SharedDefinitions.Presentation.Controllers;

namespace ProductService.API.Controllers;

/// <summary>
/// BrandsController.
/// </summary>
/// <param name="mediator">Injected IMediator.</param>
/// <param name="mapper">Injected IMapper.</param>
/// <param name="logger">Injected ILogger.</param>
/// <param name="exceptionHandlingService">Injected IExceptionHandlingService.</param>
[Route("[controller]")]
public class BrandsController(
    IMediator mediator,
    IMapper mapper,
    ILogger<BrandsController> logger,
    IExceptionHandlingService exceptionHandlingService)
    : ResultControllerBase<BrandsController>(mediator, mapper, logger, exceptionHandlingService)
{
    /// <summary>
    /// Gets a list of Brands.
    /// </summary>
    /// <returns>The list of Brands.</returns>
    [HttpGet]
    [RoleAuthorize]
    public async Task<IActionResult> GetAll() =>
        await HandleRequestAsync<GetBrandsListQuery, List<BrandDto>, List<BrandResponse>>();

    /// <summary>
    /// Gets a Brand by its Id.
    /// </summary>
    /// <param name="id">Brand Id.</param>
    /// <returns>The Brand Aggregate.</returns>
    [HttpGet("{id:long}")]
    [RoleAuthorize]
    public async Task<IActionResult> GetById(long id) =>
        await HandleRequestAsync<GetBrandByIdQuery, BrandDto, BrandResponse>(id);

    /// <summary>
    /// Add a Brand to the Brand Repository.
    /// </summary>
    /// <param name="request">Brand data.</param>
    /// <returns>The Brand instance created with Id.</returns>
    [HttpPost]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> Add(AddBrandRequest request) =>
        await HandleRequestAsync<AddBrandCommand, BrandDto, BrandResponse>(request);

    /// <summary>
    /// Updates a Brand in the Brand Repository.
    /// </summary>
    /// <param name="request">Brand data.</param>
    /// <returns>The Brand instance created with Id.</returns>
    [HttpPut]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> Update(UpdateBrandRequest request) =>
        await HandleRequestAsync<UpdateBrandCommand, BrandDto, BrandResponse>(request);

    /// <summary>
    /// Deletes a Brand from the Brand Repository.
    /// </summary>
    /// <param name="id">Brand Id.</param>
    /// <returns>The Action Result of the delete operation.</returns>
    [HttpDelete("{id:long}")]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> Delete(long id) =>
        await HandleRequestAsync<DeleteBrandCommand, Result, object>(id);
}