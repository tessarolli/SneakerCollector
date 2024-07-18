using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using Dapr.Client;
using NSubstitute;
using SneakerCollector.Services.ProductService.Domain.Products.ValueObjects;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Shoes;
using ProductService.Infrastructure.Dtos;
using ProductService.Infrastructure.Repositories;
using SharedDefinitions.Application.Abstractions.Services;
using SharedDefinitions.Infrastructure.Abstractions;

namespace ProductService.UnitTests.Infrastructure;

public class ProductRepositoryTests
{
    private readonly IShoeRepository _userRepository;
    private readonly IDapperUtility _dapper;

    public ProductRepositoryTests()
    {
        _dapper = Substitute.For<IDapperUtility>();
        var logger = Substitute.For<ILogger<ShoeRepository>>();
        var daprClient = Substitute.For<DaprClient>();
        var cacheService = Substitute.For<ICacheService>();
        _userRepository = new ShoeRepository(_dapper, logger, daprClient, cacheService);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingProduct_ReturnsProduct()
    {
        // Arrange
        var productId = new ShoeId(1);
        var dbProduct = new ShoeDb(1, 1, "Test Shoe", "Test Description", 0, 0, 0, DateTime.UtcNow);
        _dapper.QueryFirstOrDefaultAsync<ShoeDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>()).Returns(Task.FromResult<ShoeDb?>(dbProduct));

        // Act
        var result = await _userRepository.GetByIdAsync(productId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(productId);
        result.Value.Name.Should().Be("Test Shoe");
        result.Value.Description.Should().Be("Test Description");
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<ShoeDb>
        {
            new(1, 1, "Test Shoe 1", "Test Description", 0, 0, 0, DateTime.UtcNow),
            new(2, 1, "Test Shoe 2", "Test Description", 0, 0, 0, DateTime.UtcNow),
        };
        _dapper.QueryAsync<ShoeDb>(Arg.Any<string>()).Returns(Task.FromResult<IEnumerable<ShoeDb>>(products));

        // Act
        var result = await _userRepository.GetAllAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
    }

    [Fact]
    public async Task AddAsync_ValidProduct_ReturnsAddedProduct()
    {
        // Arrange
        var newProduct = Shoe.Create(null, "New Shoe");
        _dapper
            .ExecuteScalarAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
            .Returns(Task.FromResult<long>(1));
        _dapper
            .QueryFirstOrDefaultAsync<ShoeDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
            .Returns(Task.FromResult<ShoeDb?>(new ShoeDb(1, 1, "New Shoe", "", 0, 0, 0, DateTime.UtcNow)));

        // Act
        var result = await _userRepository.AddAsync(newProduct.Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Value.Should().Be(1);
        result.Value.Name.Should().Be("New Shoe");
    }

    [Fact]
    public async Task UpdateAsync_ExistingProduct_ReturnsUpdatedProduct()
    {
        // Arrange
        var existingProduct = Shoe.Create(new ShoeId(1), "Existing Shoe");
        var dbProduct = new ShoeDb(1, 1, "Existing Shoe", "", 0, 0, 0, DateTime.UtcNow);
        _dapper
            .BeginTransaction()
            .Returns(Substitute.For<DbTransaction>());
        _dapper
            .QueryFirstOrDefaultAsync<ShoeDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
            .Returns(Task.FromResult<ShoeDb?>(dbProduct));
        _dapper
           .ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
           .Returns(Task.FromResult<long>(1));

        // Act
        var result = await _userRepository.UpdateAsync(existingProduct.Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be("Existing Shoe");
    }

    [Fact]
    public async Task RemoveAsync_ExistingProductId_RemovesProduct()
    {
        // Arrange
        var productId = new ShoeId(1);
        _dapper
             .BeginTransaction()
             .Returns(Substitute.For<DbTransaction>());
        _dapper
             .ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
             .Returns(Task.FromResult<long>(1));

        // Act
        var result = await _userRepository.RemoveAsync(productId);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}