// <copyright file="IPagingSortingFilteringService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Application.Models;namespace SharedDefinitions.Infrastructure.Services;
/// <summary>
/// Interface for a service that provides functionality for paging, sorting, and filtering data.
/// </summary>
public interface IPagingSortingFilteringService{
    /// <summary>
    /// Builds a complete query by combining the base query with pagination, sorting, and optional table alias and default sort field.
    /// </summary>
    /// <param name="baseQuery">The base query to build upon.</param>
    /// <param name="request">The request containing pagination and sorting information.</param>
    /// <param name="tableAlias">Optional table alias to be used in the query.</param>
    /// <param name="defaultSortField">Optional default field to use for sorting if not specified in the request.</param>
    /// <returns>The complete query string with pagination and sorting applied.</returns>
    string BuildCompleteQuery(string baseQuery, PagedAndSortedResultRequest request, string tableAlias = "", string defaultSortField = "id");

    /// <summary>
    /// Builds an ORDER BY clause based on the provided PagedAndSortedResultRequest object, table alias, and default sort field.
    /// </summary>
    /// <param name="request">The request containing pagination and sorting information.</param>
    /// <param name="tableAlias">Optional table alias to be used in the query.</param>
    /// <param name="defaultSortField">Optional default field to use for sorting if not specified in the request.</param>
    /// <returns>String with Order By Clause.</returns>
    string BuildOrderByClause(PagedAndSortedResultRequest request, string tableAlias = "", string defaultSortField = "id");

    /// <summary>
    /// Builds a SQL paging clause based on the provided PagedAndSortedResultRequest object.
    /// </summary>
    /// <param name="request">The request containing pagination and sorting information.</param>
    /// <returns>String with Paging.</returns>
    string BuildPagingClause(PagedAndSortedResultRequest request);

    /// <summary>
    /// Builds a WHERE clause based on the provided PagedAndSortedResultRequest and optional table alias.
    /// </summary>
    /// <param name="request">The request containing pagination and sorting information.</param>
    /// <param name="tableAlias">Optional table alias to be used in the query.</param>
    /// <returns>String with Where Clause.</returns>
    string BuildWhereClause(PagedAndSortedResultRequest request, string tableAlias = "");}