// <copyright file="IShoeRepository.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data.Common;
using FluentResults;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.ValueObjects;

namespace ProductService.Application.Abstractions.Repositories;

/// <summary>
/// The Shoe Repository Interface.
/// </summary>
public interface IShoeRepository
{
    /// <summary>
    /// Get an Shoe aggregate by Id.
    /// </summary>
    /// <param name="id">The Shoe Id.</param>
    /// <param name="transaction">The Transaction.</param>
    /// <returns>A Result with the Shoe Aggregate, or a error message.</returns>
    Task<Result<Shoe>> GetByIdAsync(ShoeId id, DbTransaction? transaction = null);

    /// <summary>
    /// Gets a List of all Shoes from the repository.
    /// </summary>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<List<Shoe>>> GetAllAsync();

    /// <summary>
    /// Add an Shoe into the Repository.
    /// </summary>
    /// <param name="shoe">The Shoe to Add.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<Shoe>> AddAsync(Shoe shoe);

    /// <summary>
    /// Update the Shoe in the Repository.
    /// </summary>
    /// <param name="shoe">The Shoe to Update.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result<Shoe>> UpdateAsync(Shoe shoe);

    /// <summary>
    /// Remove the Shoe from the Repository.
    /// </summary>
    /// <param name="shoeId">The Shoe to Remove.</param>
    /// <returns>A Result indicating the status of this operation.</returns>
    Task<Result> RemoveAsync(ShoeId shoeId);
}
