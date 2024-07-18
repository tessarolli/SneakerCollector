// <copyright file="IPostgresSqlConnectionFactory.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data.Common;

namespace SharedDefinitions.Infrastructure.Abstractions;

/// <summary>
/// Defines the Factory method for Creating a Connection to a RDBMS.
/// </summary>
/// <typeparam name="T">The Type of the connection.</typeparam>
public interface ISqlConnectionFactory<out T>
    where T : DbConnection
{
    /// <summary>
    /// Creates an instance of the <typeparamref name="T"/> class.
    /// </summary>
    /// <returns>The instace of the NpgsqlConnection class.</returns>
    T CreateConnection();
}