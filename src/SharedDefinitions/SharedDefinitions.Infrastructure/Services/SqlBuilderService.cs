// <copyright file="SqlBuilderService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using Dapper;
using FluentResults;
using SharedDefinitions.Application.Models;

namespace SharedDefinitions.Infrastructure.Services;

/// <summary>
/// Represents a service for building SQL queries.
/// </summary>
public class SqlBuilderService : ISqlBuilderService
{
    /// <inheritdoc/>
    public Result<(string querySql, string counterSql, Dictionary<string, object> parameters)> BuildPagedQuery(
            string fields,
            string tables,
            PagedAndSortedResultRequest request,
            string searchColumns,
            string defaultOrderBy,
            Dictionary<string, string> validSortFields)
    {
        var sqlBuilder = new SqlBuilder();
        var selector = sqlBuilder.AddTemplate($"SELECT {fields} FROM {tables} /**where**/ /**orderby**/");
        var counter = sqlBuilder.AddTemplate($"SELECT COUNT(*) FROM {tables} /**where**/");
        var parameters = new Dictionary<string, object>
        {
            { "Offset", request.Offset },
            { "Limit", request.Limit },
        };

        AddSearchCondition(sqlBuilder, parameters, request.Search, searchColumns);

        var orderByResult = AddOrderBy(sqlBuilder, request.Sort, defaultOrderBy, validSortFields);
        if (orderByResult.IsFailed)
        {
            return Result.Fail<(string, string, Dictionary<string, object>)>(orderByResult.Errors);
        }

        var sql = $@"
                {selector.RawSql} 
                OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY;";

        return Result.Ok((sql, counter.RawSql, parameters));
    }

    /// <summary>
    /// Adds search conditions to the SQL query based on the provided filters and search columns.
    /// </summary>
    private static void AddSearchCondition(
        SqlBuilder sqlBuilder,
        Dictionary<string, object> parameters,
        string filters,
        string searchColumns)
    {
        if (!string.IsNullOrWhiteSpace(filters))
        {
            var searchConditions = searchColumns.Split(',')
                .Select(column => $"LOWER({column.Trim()}) LIKE LOWER(@Search)")
                .ToList();
            sqlBuilder.Where($"({string.Join(" OR ", searchConditions)})");
            parameters.Add("Search", $"%{filters}%");
        }
    }

    /// <summary>
    /// Adds an ORDER BY clause to the SQL query based on the provided sort parameter or defaultOrderBy if sort is not provided.
    /// </summary>
    /// <param name="sqlBuilder">The SqlBuilder object to add the ORDER BY clause to.</param>
    /// <param name="sort">The sort parameter to determine the column and direction for ordering.</param>
    /// <param name="defaultOrderBy">The default ORDER BY clause to use if sort is not provided.</param>
    /// <param name="validSortFields">A list of valid fields that can be used for sorting.</param>
    /// <returns>A Result indicating success or failure with an error message.</returns>
    private static Result AddOrderBy(
        SqlBuilder sqlBuilder,
        string sort,
        string defaultOrderBy,
        Dictionary<string, string> validSortFields)
    {
        if (!string.IsNullOrWhiteSpace(sort))
        {
            var (column, direction) = ParseSort(sort);
            if (!validSortFields.TryGetValue(column, out string? value))
            {
                return Result.Fail($"Invalid sort field: {column}");
            }

            sqlBuilder.OrderBy($"{value} {direction}");
        }
        else
        {
            sqlBuilder.OrderBy(defaultOrderBy);
        }

        return Result.Ok();
    }

    /// <summary>
    /// Parses the sort string to extract the column and direction.
    /// </summary>
    /// <param name="sort">The sort string containing the column and direction information.</param>
    /// <returns>
    /// A tuple containing the extracted column and direction.
    /// </returns>
    private static (string column, string direction) ParseSort(string sort)
    {
        var direction = sort.StartsWith('-')
            ? "DESC"
            : "ASC";
        var column = sort.TrimStart('-', '+');
        return (column, direction);
    }
}