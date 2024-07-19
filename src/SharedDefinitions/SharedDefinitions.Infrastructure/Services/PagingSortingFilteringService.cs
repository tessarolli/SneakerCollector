// <copyright file="PagingSortingFilteringService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Text;
using SharedDefinitions.Application.Models;

namespace SharedDefinitions.Infrastructure.Services;

/// <summary>
/// Represents a service for handling paging, sorting, and filtering operations.
/// Implements the IPagingSortingFilteringService interface.
/// </summary>
public sealed class PagingSortingFilteringService : IPagingSortingFilteringService
{
    /// <inheritdoc/>
    public string BuildWhereClause(PagedAndSortedResultRequest request, string tableAlias = "")
    {
        if (request.Filters == null || request.Filters.Count == 0)
        {
            return string.Empty;
        }

        var whereClauses = new List<string>();
        foreach (var filter in request.Filters)
        {
            string columnName = string.IsNullOrEmpty(tableAlias) ? filter.Key : $"{tableAlias}.{filter.Key}";
            whereClauses.Add($"{columnName} = @{filter.Key}");
        }

        return $"WHERE {string.Join(" AND ", whereClauses)}";
    }

    /// <inheritdoc/>
    public string BuildOrderByClause(PagedAndSortedResultRequest request, string tableAlias = "", string defaultSortField = "id")
    {
        if (string.IsNullOrEmpty(request.SortField))
        {
            return $"ORDER BY {(string.IsNullOrEmpty(tableAlias) ? defaultSortField : $"{tableAlias}.{defaultSortField}")}";
        }

        string columnName = string.IsNullOrEmpty(tableAlias) ? request.SortField : $"{tableAlias}.{request.SortField}";
        return $"ORDER BY {columnName} {(request.SortOrder?.ToUpper() == "DESC" ? "DESC" : "ASC")}";
    }

    /// <inheritdoc/>
    public string BuildPagingClause(PagedAndSortedResultRequest request)
    {
        return $"OFFSET {(request.Page - 1) * request.PageSize} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
    }

    /// <inheritdoc/>
    public string BuildCompleteQuery(string baseQuery, PagedAndSortedResultRequest request, string tableAlias = "", string defaultSortField = "id")
    {
        var queryBuilder = new StringBuilder(baseQuery);

        string whereClause = BuildWhereClause(request, tableAlias);
        if (!string.IsNullOrEmpty(whereClause))
        {
            queryBuilder.Append(' ').Append(whereClause);
        }

        string orderByClause = BuildOrderByClause(request, tableAlias, defaultSortField);
        queryBuilder.Append(' ').Append(orderByClause);

        string pagingClause = BuildPagingClause(request);
        queryBuilder.Append(' ').Append(pagingClause);

        return queryBuilder.ToString();
    }
}