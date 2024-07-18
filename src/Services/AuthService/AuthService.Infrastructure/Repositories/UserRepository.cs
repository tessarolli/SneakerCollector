// <copyright file="UserRepository.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Contracts.Enums;
using AuthService.Domain.Users;
using AuthService.Domain.Users.ValueObjects;
using AuthService.Infrastructure.Dtos;
using FluentResults;
using Microsoft.Extensions.Logging;
using SharedDefinitions.Application.Common.Errors;
using SharedDefinitions.Domain.Common.Abstractions;
using SharedDefinitions.Infrastructure.Abstractions;

namespace AuthService.Infrastructure.Repositories;

/// <summary>
/// The User Repository.
/// </summary>
/// <param name="dapperUtility">IDapperUtility to inject.</param>
/// <param name="passwordHasher">IPasswordHashingService to inject.</param>
/// <param name="logger">ILogger to inject.</param>
public class UserRepository(IDapperUtility dapperUtility, ILogger<UserRepository> logger, IPasswordHashingService passwordHasher) : IUserRepository
{
    private readonly ILogger _logger = logger;
    private readonly IPasswordHashingService _passwordHasher = passwordHasher;
    private readonly IDapperUtility _db = dapperUtility;

    /// <inheritdoc/>
    public async Task<Result<User>> GetByIdAsync(long id)
    {
        var sql = "SELECT * FROM users WHERE id = @id";

        var user = await _db.QueryFirstOrDefaultAsync<UserDb>(sql, new { id });

        return CreateUserResultFromUserDB(user);
    }

    /// <inheritdoc/>
    public async Task<List<User>> GetByIdsAsync(long[] ids)
    {
        _logger.LogInformation("UserRepository.GetByIdsAsync({Email})", string.Join(',', ids));

        var sql = "SELECT * FROM users WHERE id = ANY(@ids)";

        var parameters = new
        {
            ids,
        };

        var users = await _db.QueryAsync<UserDb>(sql, parameters);

        return users
            .Select(CreateUserResultFromUserDB)
            .Where(x => x.IsSuccess)
            .Select(x => x.Value)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<Result<List<User>>> GetAllAsync()
    {
        var sql = "SELECT * FROM users";

        var users = await _db.QueryAsync<UserDb>(sql, null);

        return users
            .Select(CreateUserResultFromUserDB)
            .Where(x => x.IsSuccess)
            .Select(x => x.Value)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<Result<User>> GetByEmailAsync(string email)
    {
        _logger.LogInformation("UserRepository.GetByEmailAsync({Email})", email);

        var sql = "SELECT * FROM users WHERE email = @email";

        var user = await _db.QueryFirstOrDefaultAsync<UserDb>(sql, new { email });

        return CreateUserResultFromUserDB(user);
    }

    /// <inheritdoc/>
    public async Task<Result<User>> AddAsync(User user)
    {
        if (!user.Password.IsHashed)
        {
            user.Password.HashPassword(_passwordHasher);
        }

        var sql = "INSERT INTO users (" +
                      "first_name," +
                      "last_name," +
                      "email," +
                      "password," +
                      "role) " +
                  "VALUES (" +
                      "@FirstName," +
                      "@LastName," +
                      "@Email," +
                      "@Password," +
                      "@Role) " +
                  "RETURNING " +
                      "id";

        var parameters = new
        {
            user.FirstName,
            user.LastName,
            user.Email,
            Password = user.Password.HashedPassword,
            Role = (int)user.Role,
        };

        var newId = await _db.ExecuteScalarAsync(sql, parameters);

        return await GetByIdAsync(newId);
    }

    /// <inheritdoc/>
    public async Task<Result<User>> UpdateAsync(User user)
    {
        if (!user.Password.IsHashed)
        {
            user.Password.HashPassword(_passwordHasher);
        }

        var parameters = new
        {
            p_id = user.Id.Value,
            p_first_name = user.FirstName,
            p_last_name = user.LastName,
            p_email = user.Email,
            p_password = user.Password.HashedPassword,
            p_role = user.Role,
        };

        await _db.ExecuteAsync("sp_update_user", parameters, CommandType.StoredProcedure);

        return Result.Ok(user);
    }

    /// <inheritdoc/>
    public async Task<Result> RemoveAsync(UserId userId)
    {
        var parameters = new
        {
            removing_user_id = userId.Value,
        };

        await _db.ExecuteAsync("remove_user", parameters, CommandType.StoredProcedure);

        return Result.Ok();
    }

    /// <summary>
    /// Creates a new instance of the User Domain Model Entity, using the Create factory method.
    /// </summary>
    /// <param name="user">The User Data Transfer Object.</param>
    /// <returns>An Result indicating the status of the operation.</returns>
    private static Result<User> CreateUserResultFromUserDB(UserDb? user)
    {
        if (user is null)
        {
            return Result.Fail(new NotFoundError($"User not found."));
        }

        var userDomainModel = User.Create(
                user.id,
                user.first_name,
                user.last_name,
                user.email,
                user.password,
                (Roles)user.role);

        if (!userDomainModel.IsSuccess)
        {
            return Result.Fail(userDomainModel.Errors);
        }

        return Result.Ok(userDomainModel.Value);
    }
}