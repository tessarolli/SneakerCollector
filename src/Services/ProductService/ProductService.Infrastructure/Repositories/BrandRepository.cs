// <copyright file="BrandRepository.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data;
using System.Data.Common;
using FluentResults;
using Microsoft.Extensions.Logging;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Brands;
using ProductService.Domain.Brands.ValueObjects;
using ProductService.Infrastructure.Dtos;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Application.Common.Errors;
using SharedDefinitions.Application.Models;
using SharedDefinitions.Infrastructure.Abstractions;

namespace ProductService.Infrastructure.Repositories;

/// <summary>
/// The Brand Repository Implementation.
/// </summary>
/// <param name="dapperUtility">IDapperUtility to inject.</param>
/// <param name="logger">ILogger to inject.</param>
/// <param name="sqlBuilderService">ISqlBuilderService to inject.</param>
public class BrandRepository(
    IDapperUtility dapperUtility,
    ILogger<BrandRepository> logger,
    ISqlBuilderService sqlBuilderService) : IBrandRepository
{
    private readonly IDapperUtility _db = dapperUtility ?? throw new ArgumentNullException(nameof(dapperUtility));
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ISqlBuilderService _sqlBuilderService = sqlBuilderService;

    /// <inheritdoc/>
    public async Task<Result<Brand>> GetByIdAsync(BrandId id, DbTransaction? transaction = null)
    {
        _logger.LogInformation("BrandRepository.GetByIdAsync({Id})", id.Value);

        const string sql = @"
            SELECT 
                b.*
            FROM 
                brands b
            WHERE 
                b.id = @brand_id;";

        var brand = await _db.QueryFirstOrDefaultAsync<BrandDb>(sql, new { brand_id = id.Value }, transaction: transaction);

        if (brand is null)
        {
            return new NotFoundError($"Brand with id {id.Value} not found.");
        }

        var domainModelResult = MapToDomainModel(brand);
        if (!domainModelResult.IsSuccess)
        {
            return Result.Fail(domainModelResult.Errors);
        }

        return domainModelResult.Value;
    }

    /// <inheritdoc/>
    public async Task<Result<PagedResult<Brand>>> GetAllAsync(PagedAndSortedResultRequest? request)
    {
        _logger.LogInformation("BrandRepository.GetAllAsync");
        var transaction = _db.BeginTransaction();
        request ??= new();
        Dictionary<string, string> validSortFields = new()
        {
            { "id", "b.id" },
            { "name", "b.name" },
        };

        var buildResult = _sqlBuilderService.BuildPagedQuery(
                "b.*",
                "brands b",
                request,
                "b.Name",
                "b.Id",
                validSortFields);

        if (buildResult.IsFailed)
        {
            return Result.Fail<PagedResult<Brand>>(buildResult.Errors);
        }

        try
        {
            var (querySql, counterSql, parameters) = buildResult.Value;

            var brands = await _db.QueryAsync<BrandDb>(
                querySql,
                parameters,
                transaction: transaction);

            var totalCount = await _db.ExecuteScalarAsync(counterSql, parameters, transaction: transaction);

            var domainModelResults = brands.Select(MapToDomainModel);

            var validDomainModels = domainModelResults.Where(mapped => mapped.IsSuccess).Select(mapped => mapped.Value).ToList();

            return Result.Ok(new PagedResult<Brand>
            {
                Items = validDomainModels,
                TotalCount = (int)totalCount,
                Offset = request.Offset,
                Limit = request.Limit,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on Brands Repository GetAll: {Message}", ex.Message);
        }
        finally
        {
            _db.CloseTransaction(transaction);
        }

        return Result.Fail("Internal Server Error fetching Brands.");
    }

    /// <inheritdoc/>
    public async Task<Result<Brand>> AddAsync(Brand brand)
    {
        _logger.LogInformation("BrandRepository.AddAsync({Brand})", brand.Name);

        using var trans = _db.BeginTransaction();

        try
        {
            var insertSql = @"
                INSERT INTO 
                    brands (name)
                VALUES 
                    (@Name)
                RETURNING
                    id";

            var parameters = new
            {
                brand.Name,
            };

            var newBrandId = await _db.ExecuteScalarAsync(insertSql, parameters, transaction: trans);

            _db.CloseTransaction(trans, true);

            return await GetByIdAsync(new BrandId(newBrandId));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on adding brand to repository");

            trans.Rollback();

            _db.CloseTransaction(trans);

            return Result.Fail("Error on adding brand to repository");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Brand>> UpdateAsync(Brand brand)
    {
        _logger.LogInformation("BrandRepository.UpdateAsync({Brand})", brand.Name);

        using var trans = _db.BeginTransaction();

        try
        {
            var updateSql = @"
                 UPDATE 
                     brands
                 SET 
                     name = @Name
                 WHERE 
                     id = @Id";

            var parameters = new
            {
                Id = brand.Id.Value,
                brand.Name,
            };

            await _db.ExecuteAsync(updateSql, parameters, transaction: trans);

            trans.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on update brand in the repository");

            trans.Rollback();
        }

        _db.CloseTransaction(trans);

        return await GetByIdAsync(brand.Id);
    }

    /// <inheritdoc/>
    public async Task<Result> RemoveAsync(BrandId brandId)
    {
        _logger.LogInformation("BrandRepository.RemoveAsync({BrandId})", brandId);

        using var trans = _db.BeginTransaction();

        try
        {
            var deleteDocumentAccessSql = @"
                DELETE FROM 
                    brands
                WHERE 
                    id = @brandId";

            var parameter = new { brandId = brandId.Value };

            await _db.ExecuteAsync(deleteDocumentAccessSql, parameter, transaction: trans);

            trans.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error removing brand from repository");

            trans.Rollback();
        }

        _db.CloseTransaction(trans);

        return Result.Ok();
    }

    /// <summary>
    /// Map the Brand Data Transfer Object to a Result of Brand Domain Model.
    /// </summary>
    /// <param name="brandDb">Brand Db Dto.</param>
    /// <returns>The Result of Brand Domain Model.</returns>
    private Result<Brand> MapToDomainModel(BrandDb brandDb)
    {
        var brandResult = Brand.Create(new(brandDb.id), brandDb.name);
        if (brandResult.IsFailed)
        {
            return Result.Fail(brandResult.Errors);
        }

        return brandResult.Value;
    }
}