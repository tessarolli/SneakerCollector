// <copyright file="ISqlConnectionFactory.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Data.Common;

namespace SharedDefinitions.Infrastructure.Abstractions;

/// <summary>
/// Defines the Factory method for Creating a Connection to a RDBMS.
/// </summary>
public interface ISqlConnectionFactory
{
    /// <summary>
    /// Creates an instance of the DbConnection class.
    /// </summary>
    /// <returns>The instace of the DbConnection class.</returns>
    DbConnection CreateConnection();
}