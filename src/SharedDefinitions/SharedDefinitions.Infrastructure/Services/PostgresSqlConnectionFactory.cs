// <copyright file="PostgresSqlConnectionFactory.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data.Common;
using System.Text;
using Npgsql;
using SharedDefinitions.Infrastructure.Abstractions;

namespace SharedDefinitions.Infrastructure.Services;

/// <summary>
/// Postgres Sql Connection Factory.
/// </summary>
public sealed class PostgresSqlConnectionFactory : ISqlConnectionFactory
{
    /// <inheritdoc/>
    public DbConnection CreateConnection()
    {
        var encoded = "U2VydmVyPWJhYmFyLmRiLmVsZXBoYW50c3FsLmNvbTtQb3J0PTU0MzI7RGF0YWJhc2U9Ynd2dW12Z2w7VXNlciBJZD1id3Z1bXZnbDtQYXNzd29yZD1BOWNqV2pYYWlmN1poNGNrcDRIV2k0VllXVndHRGNnODs=";
        var connectionString = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        return new NpgsqlConnection(connectionString);
    }
}