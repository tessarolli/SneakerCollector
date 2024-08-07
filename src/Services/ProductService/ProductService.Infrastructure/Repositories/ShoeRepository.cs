﻿// <copyright file="ShoeRepository.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data;
using System.Data.Common;
using Dapr.Client;
using FluentResults;
using Microsoft.Extensions.Logging;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Brands;
using ProductService.Domain.Dtos;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.Enums;
using ProductService.Domain.Shoes.ValueObjects;
using ProductService.Infrastructure.Dtos;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Application.Common.Errors;
using SharedDefinitions.Application.Models;
using SharedDefinitions.Infrastructure.Abstractions;

namespace ProductService.Infrastructure.Repositories;

/// <summary>
/// The shoeDb Repository Implementation.
/// </summary>
/// <param name="dapperUtility">IDapperUtility to inject.</param>
/// <param name="logger">ILogger to inject.</param>
/// <param name="dapr">DaprClient to inject.</param>
/// <param name="sqlBuilderService">ISqlBuilderService to inject.</param>
public class ShoeRepository(
    IDapperUtility dapperUtility,
    ILogger<ShoeRepository> logger,
    DaprClient dapr,
    ISqlBuilderService sqlBuilderService)
    : IShoeRepository
{
    private readonly IDapperUtility _db = dapperUtility ?? throw new ArgumentNullException(nameof(dapperUtility));
    private readonly DaprClient _dapr = dapr ?? throw new ArgumentNullException(nameof(dapr));
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ISqlBuilderService _sqlBuilderService = sqlBuilderService;

    /// <inheritdoc/>
    public async Task<Result<Shoe>> GetByIdAsync(ShoeId id, DbTransaction? transaction = null)
    {
        _logger.LogInformation("ShoeRepository.GetByIdAsync({Id})", id.Value);

        const string sql = @"
            SELECT 
                s.*, b.*
            FROM 
                shoes s
            JOIN
                brands b ON s.brand_id = b.id
            WHERE 
                s.id = @shoe_id;";

        var (shoe, brand) = (await _db.QueryAsync<ShoeDb, BrandDb, (ShoeDb, BrandDb)>(
            sql,
            (shoe, brand) => (shoe, brand),
            new { shoe_id = id.Value },
            transaction: transaction))
            .FirstOrDefault();

        if (shoe is null)
        {
            return new NotFoundError($"Shoe with id {id.Value} not found.");
        }

        var domainModelResult = MapToDomainModel(shoe, brand);
        if (!domainModelResult.IsSuccess)
        {
            return Result.Fail(domainModelResult.Errors);
        }

        return domainModelResult.Value;
    }

    /// <inheritdoc/>
    public async Task<Result<PagedResult<Shoe>>> GetAllAsync(PagedAndSortedResultRequest? request)
    {
        _logger.LogInformation("ShoeRepository.GetAllAsync");
        var transaction = _db.BeginTransaction();
        request ??= new();
        Dictionary<string, string> validSortFields = new()
        {
            { "id", "s.id" },
            { "brandname", "b.name" },
            { "shoename", "s.name" },
            { "year", "s.launch_year" },
            { "price", "s.price_amount" },
            { "rating", "s.rating" },
            { "size", "s.size_value" },
            { "createdatutc", "s.created_at_utc" },
        };

        var buildResult = _sqlBuilderService.BuildPagedQuery(
                "s.*, b.*",
                "shoes s JOIN brands b ON s.brand_id = b.id",
                request,
                "s.Name, b.Name",
                "s.Id",
                validSortFields);

        if (buildResult.IsFailed)
        {
            return Result.Fail<PagedResult<Shoe>>(buildResult.Errors);
        }

        try
        {
            var (querySql, counterSql, parameters) = buildResult.Value;

            var shoes = await _db.QueryAsync<ShoeDb, BrandDb, (ShoeDb, BrandDb)>(
           querySql,
           (shoe, brand) => (shoe, brand),
           parameters,
           transaction: transaction);

            var totalCount = await _db.ExecuteScalarAsync(counterSql, parameters, transaction: transaction);

            var domainModelResults = shoes.Select((tuple) => MapToDomainModel(tuple.Item1, tuple.Item2));

            var validDomainModels = domainModelResults.Where(mapped => mapped.IsSuccess).Select(mapped => mapped.Value).ToList();

            return Result.Ok(new PagedResult<Shoe>
            {
                Items = validDomainModels,
                TotalCount = (int)totalCount,
                Offset = request.Offset,
                Limit = request.Limit,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on Shoes Repository GetAll: {Message}", ex.Message);
        }
        finally
        {
            _db.CloseTransaction(transaction);
        }

        return Result.Fail("Internal Server Error fetching Shoes.");
    }

    /// <inheritdoc/>
    public async Task<Result<Shoe>> AddAsync(Shoe shoe)
    {
        _logger.LogInformation("ShoeRepository.AddAsync({Shoe})", shoe.Name);

        using var trans = _db.BeginTransaction();

        try
        {
            var insertSql = @"
                INSERT INTO 
                    shoes (name, owner_id, brand_id, size_value, size_unit, price_amount, price_currency, launch_year, rating, created_at_utc)
                VALUES 
                    (@Name, @OwnerId, @BrandId, @Value, @Unit, @Amount, @Currency, @Year, @Rating, @CreatedAtUtc)
                RETURNING
                    id";

            var parameters = new
            {
                shoe.Name,
                shoe.OwnerId,
                shoe.BrandId,
                shoe.Size.Value,
                shoe.Size.Unit,
                shoe.Price.Amount,
                shoe.Price.Currency,
                shoe.Year,
                shoe.Rating,
                shoe.CreatedAtUtc,
            };

            var newShoeId = await _db.ExecuteScalarAsync(insertSql, parameters, transaction: trans);

            _db.CloseTransaction(trans, true);

            return await GetByIdAsync(new ShoeId(newShoeId));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on adding shoe to repository");

            trans.Rollback();

            _db.CloseTransaction(trans);

            return Result.Fail("Error on adding shoe to repository");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Shoe>> UpdateAsync(Shoe shoe)
    {
        _logger.LogInformation("ShoeRepository.UpdateAsync({Shoe})", shoe.Name);

        using var trans = _db.BeginTransaction();

        try
        {
            var updateSql = @"
                 UPDATE 
                     shoes
                 SET 
                     name = @Name, 
                     owner_id = @OwnerId,
                     size_value = @Value,
                     size_unit = @Unit,
                     price_amount = @Amount,
                     price_currency = @Currency,
                     launch_year = @Year,
                     rating = @Rating,
                     created_at_utc = @CreatedAtUtc
                 WHERE 
                     id = @Id";

            var parameters = new
            {
                Id = shoe.Id.Value,
                shoe.Name,
                shoe.OwnerId,
                shoe.Size.Value,
                shoe.Size.Unit,
                shoe.Price.Amount,
                shoe.Price.Currency,
                shoe.Year,
                shoe.Rating,
                shoe.CreatedAtUtc,
            };

            await _db.ExecuteAsync(updateSql, parameters, transaction: trans);

            trans.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on update shoe in the repository");

            trans.Rollback();
        }

        _db.CloseTransaction(trans);

        return await GetByIdAsync(shoe.Id);
    }

    /// <inheritdoc/>
    public async Task<Result> RemoveAsync(ShoeId shoeId)
    {
        _logger.LogInformation("ShoeRepository.RemoveAsync({ShoeId})", shoeId);

        using var trans = _db.BeginTransaction();

        try
        {
            var deleteDocumentAccessSql = @"
                DELETE FROM 
                    shoes
                WHERE 
                    id = @shoeId";

            var parameter = new { shoeId = shoeId.Value };

            await _db.ExecuteAsync(deleteDocumentAccessSql, parameter, transaction: trans);

            trans.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error removing shoe from repository");

            trans.Rollback();
        }

        _db.CloseTransaction(trans);

        return Result.Ok();
    }

    /// <summary>
    /// Map the Shoe Data Transfer Object to a Result of Shoe Domain Model.
    /// </summary>
    /// <param name="shoeDb">Shoe Db Dto.</param>
    /// <param name="brandDb">Brand Db Dto.</param>
    /// <returns>The Result of Shoe Domain Model.</returns>
    private Result<Shoe> MapToDomainModel(ShoeDb shoeDb, BrandDb brandDb)
    {
        var brandResult = Brand.Create(new(brandDb.id), brandDb.name);
        if (brandResult.IsFailed)
        {
            return Result.Fail(brandResult.Errors);
        }

        var shoeDomainModel = Shoe.Create(
                new ShoeId(shoeDb.id),
                shoeDb.owner_id,
                shoeDb.name,
                brandResult.Value,
                new Price((Currency)shoeDb.price_currency, shoeDb.price_amount),
                new Size((ShoeSizeUnit)shoeDb.size_unit, shoeDb.size_value),
                shoeDb.launch_year,
                shoeDb.rating,
                shoeDb.created_at_utc,
                GetOwnerLazyLoader(shoeDb));

        if (!shoeDomainModel.IsSuccess)
        {
            return Result.Fail(shoeDomainModel.Errors);
        }

        return shoeDomainModel;
    }

    private Lazy<Task<UserDto?>> GetOwnerLazyLoader(ShoeDb shoe)
    {
        return new Lazy<Task<UserDto?>>(async () =>
        {
            return await _dapr.InvokeMethodAsync<UserDto>("auth-service-api", $"/authentication/getuserbyid/{shoe.owner_id}");
        });
    }
}