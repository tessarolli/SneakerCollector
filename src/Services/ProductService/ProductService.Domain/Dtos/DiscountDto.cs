// <copyright file="DiscountDto.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>


namespace SneakerCollector.Services.ProductService.Domain.Dtos;

/// <summary>
/// Data Transfer Object for the Discount Domain Model.
/// </summary>
/// <param name="ProductId">The Product Id this discount is for.</param>
/// <param name="Amount">The Discount Amount in percentage.</param>
public record DiscountDto(long ProductId, decimal Amount);