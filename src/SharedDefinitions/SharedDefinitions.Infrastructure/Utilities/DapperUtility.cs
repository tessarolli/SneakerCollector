// <copyright file="DapperUtility.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data;
using System.Data.Common;
using Dapper;
using SharedDefinitions.Infrastructure.Abstractions;
using static Dapper.SqlMapper;

namespace SharedDefinitions.Infrastructure.Utilities;

/// <summary>
/// Provides Dapper Accessibility Helpers Functunalities.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DapperUtility"/> class.
/// </remarks>
/// <param name="connectionFactory">ISqlConnectionFactory injected.</param>
public sealed class DapperUtility(ISqlConnectionFactory connectionFactory) : IDapperUtility
{
    private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> QueryAsync<T>(
        string sql,
        object? parameters = null,
        CommandType commandType = CommandType.Text,
        DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();

        var result = await connection.QueryAsync<T>(sql, parameters, commandType: commandType, transaction: transaction);

        ProperlyDisposeConnection(connection, transaction);

        return result;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Tres>> QueryAsync<T1, T2, Tres>(
        string sql,
        Func<T1, T2, Tres> mapperFunction,
        object? parameters = null,
        string splitOn = "Id",
        CommandType commandType = CommandType.Text,
        DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();

        var result = await connection.QueryAsync(sql, mapperFunction, parameters, splitOn: splitOn, commandType: commandType, transaction: transaction);

        ProperlyDisposeConnection(connection, transaction);

        return result;
    }

    /// <inheritdoc/>
    public async Task<T?> QueryFirstOrDefaultAsync<T>(
      string sql,
      object? parameters = null,
      CommandType commandType = CommandType.Text,
      DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();

        var result = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType, transaction: transaction);

        ProperlyDisposeConnection(connection, transaction);

        return result;
    }

    /// <inheritdoc/>
    public async Task<GridReader> QueryMultipleAsync(
        string sql,
        object? param = null,
        DbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();

        var result = await connection.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);

        ProperlyDisposeConnection(connection, transaction);

        return result;
    }

    /// <inheritdoc/>
    public async Task<long> ExecuteAsync(
        string sql,
        object? parameters = null,
        CommandType commandType = CommandType.Text,
        DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();

        var rowsAffectedCount = await connection.ExecuteAsync(sql, parameters, commandType: commandType, transaction: transaction);

        ProperlyDisposeConnection(connection, transaction);

        return rowsAffectedCount;
    }

    /// <inheritdoc/>
    public async Task<long> ExecuteScalarAsync(
        string sql,
        object? parameters = null,
        CommandType commandType = CommandType.Text,
        DbTransaction? transaction = null)
    {
        var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();

        var scalar = await connection.ExecuteScalarAsync<long>(sql, parameters, commandType: commandType, transaction: transaction);

        ProperlyDisposeConnection(connection, transaction);

        return scalar;
    }

    /// <inheritdoc/>
    public DbTransaction BeginTransaction()
    {
        var connection = _connectionFactory.CreateConnection();

        connection.Open();

        return connection.BeginTransaction();
    }

    /// <inheritdoc/>
    public void CloseTransaction(IDbTransaction transaction, bool commitBeforeClosing = false)
    {
        if (commitBeforeClosing)
        {
            transaction.Commit();
        }

        if (transaction.Connection is not null)
        {
            transaction.Connection.Dispose();
            transaction.Dispose();
        }
    }

    private void ProperlyDisposeConnection(IDbConnection connection, DbTransaction? transaction)
    {
        if (transaction is null)
        {
            connection.Dispose();
        }
    }
}
