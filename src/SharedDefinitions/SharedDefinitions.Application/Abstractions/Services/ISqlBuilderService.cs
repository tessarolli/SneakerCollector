// <copyright file="ISqlBuilderService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;using SharedDefinitions.Application.Models;namespace SharedDefinitions.Infrastructure.Services;/// <summary>
/// Interface for a service responsible for building SQL queries.
/// </summary>
public interface ISqlBuilderService{
    /// <summary>
    /// Builds a paged query by combining the base query with search and sorting parameters.
    /// </summary>
    /// <param name="fields">The fields to select.</param>
    /// <param name="tables">The tables to select.</param>
    /// <param name="request">The request containing paging and sorting information.</param>
    /// <param name="searchColumns">Columns to search for filtering.</param>
    /// <param name="defaultOrderBy">Default column to use for ordering if not specified in the request.</param>
    /// <param name="validSortFields">A list of valid sorting fields.</param>
    /// <returns>A new SQL query with paging and sorting applied.</returns>
    Result<(string querySql, string counterSql, Dictionary<string, object> parameters)> BuildPagedQuery(
            string fields,
            string tables,
            PagedAndSortedResultRequest request,
            string searchColumns,
            string defaultOrderBy,
            Dictionary<string, string> validSortFields);}