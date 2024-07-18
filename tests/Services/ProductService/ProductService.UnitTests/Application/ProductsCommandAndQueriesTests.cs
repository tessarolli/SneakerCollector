using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Shoes.Commands.AddShoe;
using ProductService.Application.Shoes.Commands.DeleteShoe;
using ProductService.Application.Shoes.Commands.UpdateShoe;
using ProductService.Application.Shoes.Dtos;
using ProductService.Application.Shoes.Queries.GetShoeById;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.ValueObjects;

namespace ShoeService.UnitTests.Application;

public class ShoesCommandAndQueriesTests
{
    private readonly IShoeRepository _productRepository;

    public ShoesCommandAndQueriesTests()
    {
        _productRepository = Substitute.For<IShoeRepository>();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnShoeDto()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var productDto = new ShoeDto(1, 1, "Shoe", "Description", 0, 0, 0, 0, 0, utcNow);
        var addShoeCommand = new AddShoeCommand(1, "Shoe", "Description", 0);
        var productDomainModel = Shoe.Create(new ShoeId(1), "Shoe", "Description", 0, 0, ownerId: 1, createdAtUtc: utcNow);
        _productRepository.AddAsync(Arg.Any<Shoe>()).Returns(Result.Ok(productDomainModel.Value));

        var handler = new AddShoeCommandHandler(_productRepository);

        // Act
        var result = await handler.Handle(addShoeCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(productDto);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnError()
    {
        // Arrange
        var addShoeCommand = new AddShoeCommand(1, "Shoe", "Description", 0);
        var error = new Error("Invalid Shoe Data");
        _ = Shoe.Create(null, "Shoe", "Description", 0, 0);
        _productRepository.AddAsync(Arg.Any<Shoe>()).Returns(Result.Fail(error));
        var handler = new AddShoeCommandHandler(_productRepository);

        // Act
        var result = await handler.Handle(addShoeCommand, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsSuccessResult()
    {
        // Arrange
        long productId = 1;
        var deleteCommand = new DeleteShoeCommand(productId);
        var handler = new DeleteShoeCommandHandler(_productRepository);
        _productRepository.RemoveAsync(Arg.Any<ShoeId>()).Returns(Result.Ok());

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsFailureResult()
    {
        // Arrange
        long productId = 0;
        var deleteCommand = new DeleteShoeCommand(productId);
        var handler = new DeleteShoeCommandHandler(_productRepository);
        var error = new Error("Shoe not found");
        _productRepository.RemoveAsync(Arg.Any<ShoeId>()).Returns(Result.Fail("Shoe not found"));

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_RepositoryException_ReturnsFailureResult()
    {
        // Arrange
        long productId = 2;
        var deleteCommand = new DeleteShoeCommand(productId);
        var handler = new DeleteShoeCommandHandler(_productRepository);
        var error = new Error("Repository exception");
        _productRepository.RemoveAsync(Arg.Any<ShoeId>()).Returns(Result.Fail("Repository exception"));

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsShoeDto()
    {
        // Arrange
        var handler = new UpdateShoeCommandHandler(_productRepository);
        var request = new UpdateShoeCommand(1, 1, "Shoe", "Description", 100, 5);
        var productDomainModel = Shoe.Create(new ShoeId(1), "Shoe", "Description", 5, 100, ownerId: 1);

        _productRepository.UpdateAsync(Arg.Any<Shoe>()).Returns(productDomainModel);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(1);
        result.Value.OwnerId.Should().Be(1);
        result.Value.Name.Should().Be("Shoe");
        result.Value.Description.Should().Be("Description");
        result.Value.Stock.Should().Be(5);
        result.Value.Price.Should().Be(100);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnsFailureResult()
    {
        // Arrange
        var handler = new UpdateShoeCommandHandler(_productRepository);
        var request = new UpdateShoeCommand(1, 1, "Shoe", "Description", 100, 5);
        var productDomainModel = Result.Fail<Shoe>("Invalid data.");

        _productRepository.UpdateAsync(Arg.Any<Shoe>()).Returns(productDomainModel);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsShoeDto()
    {
        // Arrange
        var productId = 1;
        var utcNow = DateTime.UtcNow;
        var getShoeByIdQuery = new GetShoeByIdQuery(productId);
        var productDto = new ShoeDto(productId, productId, "Shoe", "Description", 0, 0, 0, 0, 0, utcNow);
        _productRepository.GetByIdAsync(new ShoeId(productId)).Returns(Result.Ok(Shoe.Create(new ShoeId(productId), "Shoe", "Description", 0, 0, createdAtUtc: utcNow, ownerId: 1));
        var handler = new GetShoeByIdQueryHandler(_productRepository);

        // Act
        var result = await handler.Handle(getShoeByIdQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(productDto);
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsErrorResult()
    {
        // Arrange
        var productId = 0;
        var getShoeByIdQuery = new GetShoeByIdQuery(productId);
        _productRepository.GetByIdAsync(new ShoeId(productId)).Returns(Result.Fail(new Error("Shoe not found")));
        var handler = new GetShoeByIdQueryHandler(_productRepository);

        // Act
        var result = await handler.Handle(getShoeByIdQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("Shoe not found");
    }

    [Fact]
    public void Validate_ValidId_NoValidationErrors()
    {
        // Arrange
        var getShoeByIdQueryValidator = new GetShoeByIdQueryValidator();
        var getShoeByIdQuery = new GetShoeByIdQuery(1);

        // Act
        var result = getShoeByIdQueryValidator.Validate(getShoeByIdQuery);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldReturnListOfShoeDtos()
    {
        // Arrange
        var userRepository = Substitute.For<IShoeRepository>();
        var productsListQuery = new GetShoesListQuery();
        var productsListQueryHandler = new GetShoesListQueryHandler(userRepository);
        var lazyLoader = new Lazy<Task<DiscountDto>>(() => Task.FromResult(new DiscountDto(0, 0)));

        var mockedShoes = new List<Shoe>
        {
            Shoe.Create(new ShoeId(1), "Shoe", "Description", 0, 100, ownerId: 1, discountLazyLoader : lazyLoader !).Value,
        };

        userRepository.GetAllAsync().Returns(Result.Ok(mockedShoes));

        // Act
        var result = await productsListQueryHandler.Handle(productsListQuery, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().HaveCount(1);
        result.Value.First().Name.Should().Be("Shoe");
        result.Value.First().Description.Should().Be("Description");
        result.Value.First().OwnerId.Should().Be(1);
        result.Value.First().Price.Should().Be(100);
    }

    [Fact]
    public async Task Handle_WithNoShoes_ShouldReturnEmptyList()
    {
        // Arrange
        var userRepository = Substitute.For<IShoeRepository>();
        var productsListQuery = new GetShoesListQuery();
        var productsListQueryHandler = new GetShoesListQueryHandler(userRepository);

        userRepository.GetAllAsync().Returns(Result.Ok(new List<Shoe>()));

        // Act
        var result = await productsListQueryHandler.Handle(productsListQuery, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty();
    }
}