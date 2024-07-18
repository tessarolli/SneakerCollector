// <copyright file="IBrandRepository.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data.Common;
using FluentResults;
using ProductService.Domain.Brands;
using ProductService.Domain.Brands.ValueObjects;

namespace ProductService.Application.Abstractions.Repositories;

/// <summary>
/// The Brand Repository Interface.
/// </summary>
public interface IBrandRepository
{
    /// <summary>
    /// Get an Brand aggregate by Id.
    /// </summary>
    /// <param name="id">The Brand Id.</param>
    /// <param name="transaction">The Transaction.</param>
    /// <returns>A Result with the Brand Aggregate, or a error message.</returns>
    Task<Result<Brand>> GetByIdAsync(BrandId id, DbTransaction? transaction = null);

    /// <summary>
    /// Gets a List of all Brands from the repository.
    /// </summary>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<List<Brand>>> GetAllAsync();

    /// <summary>
    /// Add an Brand into the Repository.
    /// </summary>
    /// <param name="shoe">The Brand to Add.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<Brand>> AddAsync(Brand shoe);

    /// <summary>
    /// Update the Brand in the Repository.
    /// </summary>
    /// <param name="shoe">The Brand to Update.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<Brand>> UpdateAsync(Brand shoe);

    /// <summary>
    /// Remove the Brand from the Repository.
    /// </summary>
    /// <param name="shoeId">The Brand to Remove.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result> RemoveAsync(BrandId shoeId);
}