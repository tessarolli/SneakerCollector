// <copyright file="UserService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using ProductService.Application.Abstractions.Services;
using ProductService.Domain.Dtos;

namespace ProductService.Infrastructure.Services;

/// <summary>
/// User Service implementation.
/// </summary>
public sealed class UserService(
    DaprClient daprClient,
    IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly DaprClient _daprClient = daprClient;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <inheritdoc/>
    public async Task<UserDto?> GetUserByIdAsync(long id)
    {
        try
        {
            var request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Get, "auth-service-api", $"/users/{id}");
            var bearerToken = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(bearerToken))
            {
                return null;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            return await _daprClient.InvokeMethodAsync<UserDto>(request);
        }
        catch (Exception)
        {
            return null;
        }
    }
}