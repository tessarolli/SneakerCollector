using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using Dapr.Client;
using ProductService.Application.Abstractions.Repositories;
using SharedDefinitions.Infrastructure.Abstractions;
using ProductService.Infrastructure.Repositories;
using ProductService.Domain.Shoes.ValueObjects;
using ProductService.Infrastructure.Dtos;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.Enums;
using ProductService.Domain.Brands;
using ProductService.Domain.Brands.ValueObjects;
using SharedDefinitions.Infrastructure.Services;
using SharedDefinitions.Application.Models;

namespace ProductService.UnitTests.Infrastructure;

public class ShoeRepositoryTests
{
    private readonly IShoeRepository _shoeRepository;
    private readonly IDapperUtility _dapper;
    private readonly DaprClient _daprClient;
    private readonly ISqlBuilderService _sqlBuilderService;

    public ShoeRepositoryTests()
    {
        _dapper = Substitute.For<IDapperUtility>();
        var logger = Substitute.For<ILogger<ShoeRepository>>();
        _daprClient = Substitute.For<DaprClient>();
        _sqlBuilderService = Substitute.For<ISqlBuilderService>();
        _shoeRepository = new ShoeRepository(_dapper, logger, _daprClient, _sqlBuilderService);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingShoe_ReturnsShoe()
    {
        // Arrange
        var shoeId = new ShoeId(1);
        var dbShoe = new ShoeDb(1, 1, 1, "Test Shoe", 1, 100, 0, 10, 2022, 5, DateTime.UtcNow);
        var brandDb = new BrandDb(1, "Test Brand");

        _dapper.QueryAsync(
            Arg.Any<string>(),
            Arg.Any<Func<ShoeDb, BrandDb, (ShoeDb, BrandDb)>>(),
            Arg.Any<object>(),
            Arg.Any<string>(),
            Arg.Any<CommandType>(),
            Arg.Any<DbTransaction>()
        ).Returns(new List<(ShoeDb, BrandDb)> { (dbShoe, brandDb) });

        // Act
        var result = await _shoeRepository.GetByIdAsync(shoeId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(shoeId);
        result.Value.Name.Should().Be("Test Shoe");
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllShoes()
    {
        // Arrange
        var shoes = new List<(ShoeDb, BrandDb)>
        {
            (new ShoeDb(1, 1, 1, "Test Shoe 1", 1, 100, 10, 0, 2022, 5, DateTime.UtcNow), new BrandDb(1, "Brand 1")),
            (new ShoeDb(2, 1, 1, "Test Shoe 2", 1, 150, 11, 0, 2023, 4, DateTime.UtcNow), new BrandDb(1, "Brand 1"))
        };

        var totalCount = 2;
        var request = new PagedAndSortedResultRequest { Offset = 0, Limit = 10 };

        _dapper.QueryAsync<ShoeDb, BrandDb, (ShoeDb, BrandDb)>(
            Arg.Any<string>(),
            Arg.Any<Func<ShoeDb, BrandDb, (ShoeDb, BrandDb)>>(),
            Arg.Any<object>(),
            Arg.Any<string>(),
            Arg.Any<CommandType>(),
            Arg.Any<DbTransaction>()
        ).Returns(shoes);

        _dapper.ExecuteScalarAsync(
            Arg.Any<string>(),
            Arg.Any<object>(),
            Arg.Any<CommandType>(),
            Arg.Any<DbTransaction>()
        ).Returns(totalCount);

        _sqlBuilderService.BuildPagedQuery(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<PagedAndSortedResultRequest>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<Dictionary<string, string>>()
        ).Returns(Result.Ok((
            "querySql",
            "counterSql",
            new Dictionary<string, object>()
        )));

        // Act
        var result = await _shoeRepository.GetAllAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Items.Should().HaveCount(2);
        result.Value.TotalCount.Should().Be(totalCount);
    }


    [Fact]
    public async Task AddAsync_ValidShoe_ReturnsAddedShoe()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var brand = Brand.Create(new BrandId(1), "Brand").Value;
        var newShoe = Shoe.Create(null, 1, "New Shoe", brand, new Price(Currency.USD, 100), new Size(ShoeSizeUnit.US, 10), 2022, 5, now).Value;

        _dapper.BeginTransaction().Returns(Substitute.For<DbTransaction>());
        _dapper.ExecuteScalarAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction>())
            .Returns(1L);

        var returnedShoeDb = new ShoeDb(1, 1, 1, "New Shoe", 1, 100, 10, 0, 2022, 5, now);
        var returnedBrandDb = new BrandDb(1, "Brand");

        _dapper.QueryAsync(
            Arg.Any<string>(),
            Arg.Any<Func<ShoeDb, BrandDb, (ShoeDb, BrandDb)>>(),
            Arg.Any<object>(),
            Arg.Any<string>(),
            Arg.Any<CommandType>(),
            Arg.Any<DbTransaction>()
        ).Returns(new List<(ShoeDb, BrandDb)> { (returnedShoeDb, returnedBrandDb) });

        // Act
        var result = await _shoeRepository.AddAsync(newShoe);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Value.Should().Be(1);
        result.Value.Name.Should().Be("New Shoe");
    }

    [Fact]
    public async Task UpdateAsync_ExistingShoe_ReturnsUpdatedShoe()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var brand = Brand.Create(new BrandId(1), "Brand").Value;
        var existingShoe = Shoe.Create(new ShoeId(1), 1, "Existing Shoe", brand, new Price(Currency.USD, 100), new Size(ShoeSizeUnit.US, 10), 2022, 5, now).Value;

        _dapper.BeginTransaction().Returns(Substitute.For<DbTransaction>());
        _dapper.ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction>())
            .Returns(1L);

        var updatedShoeDb = new ShoeDb(1, 1, 1, "Existing Shoe", 1, 100, 10, 0, 2022, 5, now);
        var updatedBrandDb = new BrandDb(1, "Brand");

        _dapper.QueryAsync(
            Arg.Any<string>(),
            Arg.Any<Func<ShoeDb, BrandDb, (ShoeDb, BrandDb)>>(),
            Arg.Any<object>(),
            Arg.Any<string>(),
            Arg.Any<CommandType>(),
            Arg.Any<DbTransaction>()
        ).Returns(new List<(ShoeDb, BrandDb)> { (updatedShoeDb, updatedBrandDb) });

        // Act
        var result = await _shoeRepository.UpdateAsync(existingShoe);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be("Existing Shoe");
    }

    [Fact]
    public async Task RemoveAsync_ExistingShoeId_RemovesShoe()
    {
        // Arrange
        var shoeId = new ShoeId(1);
        _dapper.BeginTransaction().Returns(Substitute.For<DbTransaction>());
        _dapper.ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction>())
            .Returns(1L);

        // Act
        var result = await _shoeRepository.RemoveAsync(shoeId);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}