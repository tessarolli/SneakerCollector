using ProductService.Application.Abstractions.Repositories;
using ProductService.Application.Abstractions.Services;
using ProductService.Application.Shoes.Commands.AddShoe;
using ProductService.Application.Shoes.Commands.DeleteShoe;
using ProductService.Application.Shoes.Commands.UpdateShoe;
using ProductService.Application.Shoes.Dtos;
using ProductService.Application.Shoes.Queries.GetShoeById;
using ProductService.Application.Shoes.Queries.GetShoesList;
using ProductService.Domain.Brands;
using ProductService.Domain.Brands.ValueObjects;
using ProductService.Domain.Dtos;
using ProductService.Domain.Shoes;
using ProductService.Domain.Shoes.Enums;
using ProductService.Domain.Shoes.ValueObjects;
using SharedDefinitions.Application.Models;

namespace ProductService.UnitTests.Application;

public class ShoesCommandAndQueriesTests
{
    private readonly IShoeRepository _shoeRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IUserService _userService;

    public ShoesCommandAndQueriesTests()
    {
        _shoeRepository = Substitute.For<IShoeRepository>();
        _brandRepository = Substitute.For<IBrandRepository>();
        _userService = Substitute.For<IUserService>();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnShoeDto()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        UserDto? user = new() { Id = 1, FirstName = "First", LastName = "Last", Email = "email@email.com", Role = 0 };
        var shoeDto = new ShoeDto(1, 1, 1, "Shoe", "Brand", Currency.USD, 0, ShoeSizeUnit.EU, 0, 2000, 0, utcNow);
        var addShoeCommand = new AddShoeCommand(1, 1, "Shoe", Currency.USD, 0, ShoeSizeUnit.EU, 0, 2000, 0);
        var shoeDomainModel = Shoe.Create(new ShoeId(1), 1, "Shoe", Brand.Create(new(1), "Brand").Value, new Price(Currency.USD, 0), new Size(ShoeSizeUnit.EU, 0), 2000, 0, utcNow);
        _brandRepository.GetByIdAsync(Arg.Any<BrandId>()).Returns(Brand.Create(new(1), "Brand").Value);
        _shoeRepository.AddAsync(Arg.Any<Shoe>()).Returns(Result.Ok(shoeDomainModel.Value));
        _userService.GetUserByIdAsync(Arg.Any<long>()).Returns(Task.FromResult<UserDto?>(user));
        var handler = new AddShoeCommandHandler(_brandRepository, _shoeRepository, _userService);

        // Act
        var result = await handler.Handle(addShoeCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(shoeDto);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnError()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var addShoeCommand = new AddShoeCommand(1, 1, "Shoe", Currency.USD, 0, ShoeSizeUnit.EU, 0, 2000, 0);
        var error = new Error("Invalid Shoe Data");
        _ = Shoe.Create(new ShoeId(1), 1, "Shoe", Brand.Create(new(1), "Brand").Value, new Price(Currency.USD, 0), new Size(ShoeSizeUnit.EU, 0), 2000, 0, utcNow);
        _shoeRepository.AddAsync(Arg.Any<Shoe>()).Returns(Result.Fail(error));
       
        UserDto? user = new() { Id = 1, FirstName = "First", LastName = "Last", Email = "email@email.com", Role = 0 };
        _userService.GetUserByIdAsync(Arg.Any<long>()).Returns(Task.FromResult<UserDto?>(user));
        _brandRepository.GetByIdAsync(Arg.Any<BrandId>()).Returns(Brand.Create(new(1), "Brand").Value);


        var handler = new AddShoeCommandHandler(_brandRepository, _shoeRepository, _userService);

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
        long shoeId = 1;
        var deleteCommand = new DeleteShoeCommand(shoeId);
        var handler = new DeleteShoeCommandHandler(_shoeRepository);
        _shoeRepository.RemoveAsync(Arg.Any<ShoeId>()).Returns(Result.Ok());

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsFailureResult()
    {
        // Arrange
        long shoeId = 0;
        var deleteCommand = new DeleteShoeCommand(shoeId);
        var handler = new DeleteShoeCommandHandler(_shoeRepository);
        var error = new Error("Shoe not found");
        _shoeRepository.RemoveAsync(Arg.Any<ShoeId>()).Returns(Result.Fail("Shoe not found"));

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
        long shoeId = 2;
        var deleteCommand = new DeleteShoeCommand(shoeId);
        var handler = new DeleteShoeCommandHandler(_shoeRepository);
        var error = new Error("Repository exception");
        _shoeRepository.RemoveAsync(Arg.Any<ShoeId>()).Returns(Result.Fail("Repository exception"));

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
        var utcNow = DateTime.UtcNow;
        var handler = new UpdateShoeCommandHandler(_brandRepository, _shoeRepository, _userService);
        var request = new UpdateShoeCommand(1, 1, 1, "Brand", "Shoe", Currency.USD, 100, ShoeSizeUnit.EU, 5, 2000, 0);
        var shoeDomainModel = Shoe.Create(new ShoeId(1), 1, "Shoe", Brand.Create(new(1), "Brand").Value, new Price(Currency.USD, 100), new Size(ShoeSizeUnit.EU, 0), 2000, 0, utcNow);
        UserDto? user = new() { Id = 1, FirstName = "First", LastName = "Last", Email = "email@email.com", Role = 0 };
        _userService.GetUserByIdAsync(Arg.Any<long>()).Returns(Task.FromResult<UserDto?>(user));
        _brandRepository.GetByIdAsync(Arg.Any<BrandId>()).Returns(Brand.Create(new(1), "Brand").Value);
        _shoeRepository.UpdateAsync(Arg.Any<Shoe>()).Returns(shoeDomainModel);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(1);
        result.Value.OwnerId.Should().Be(1);
        result.Value.Name.Should().Be("Shoe");
        result.Value.Price.Should().Be(100);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnsFailureResult()
    {
        // Arrange
        var handler = new UpdateShoeCommandHandler(_brandRepository, _shoeRepository, _userService);
        var request = new UpdateShoeCommand(1, 1, 1, "Brand", "Shoe", Currency.USD, -1, ShoeSizeUnit.US, 1, 100, 0);
        var shoeDomainModel = Result.Fail<Shoe>("Invalid data.");
        UserDto? user = new() { Id = 1, FirstName = "First", LastName = "Last", Email = "email@email.com", Role = 0 };
        _userService.GetUserByIdAsync(Arg.Any<long>()).Returns(Task.FromResult<UserDto?>(user));
        _brandRepository.GetByIdAsync(Arg.Any<BrandId>()).Returns(Brand.Create(new(1), "Brand").Value);
        _shoeRepository.UpdateAsync(Arg.Any<Shoe>()).Returns(shoeDomainModel);

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
        var shoeId = 1;
        var utcNow = DateTime.UtcNow;
        var getShoeByIdQuery = new GetShoeByIdQuery(shoeId);
        var shoeDto = new ShoeDto(shoeId, 1, 1, "Shoe", "Brand", Currency.USD, 1, ShoeSizeUnit.US, 1, 2000, 1, utcNow);
        var shoeDomainModel = Shoe.Create(new ShoeId(shoeId), 1, "Shoe", Brand.Create(new(1), "Brand").Value, new(Currency.USD, 1), new(ShoeSizeUnit.US, 1), 2000, 1, createdAtUtc: utcNow).Value;
        _shoeRepository.GetByIdAsync(new ShoeId(shoeId)).Returns(Result.Ok(shoeDomainModel));
        var handler = new GetShoeByIdQueryHandler(_shoeRepository);

        // Act
        var result = await handler.Handle(getShoeByIdQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(shoeDto);
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsErrorResult()
    {
        // Arrange
        var shoeId = 0;
        var getShoeByIdQuery = new GetShoeByIdQuery(shoeId);
        _shoeRepository.GetByIdAsync(new ShoeId(shoeId)).Returns(Result.Fail(new Error("Shoe not found")));
        var handler = new GetShoeByIdQueryHandler(_shoeRepository);

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
        var shoesListQuery = new GetShoesListQuery(new());
        var shoesListQueryHandler = new GetShoesListQueryHandler(userRepository);
        var shoeDomainModel = Shoe.Create(new ShoeId(1), 1, "Shoe", Brand.Create(new(1), "Brand").Value, new Price(Currency.USD, 100), new Size(ShoeSizeUnit.EU, 0), 2000, 0).Value;
        var mockedShoes = new PagedResult<Shoe>
        {
            Items = [shoeDomainModel],
        };

        userRepository.GetAllAsync(Arg.Any<PagedAndSortedResultRequest>()).Returns(Result.Ok(mockedShoes));

        // Act
        var result = await shoesListQueryHandler.Handle(shoesListQuery, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Items.Count().Should().Be(1);
        result.Value.Items.First().Name.Should().Be("Shoe");
        result.Value.Items.First().OwnerId.Should().Be(1);
        result.Value.Items.First().Price.Should().Be(100);
    }

    [Fact]
    public async Task Handle_WithNoShoes_ShouldReturnEmptyList()
    {
        // Arrange
        var userRepository = Substitute.For<IShoeRepository>();
        var shoesListQuery = new GetShoesListQuery(new());
        var shoesListQueryHandler = new GetShoesListQueryHandler(userRepository);
        var mockedShoes = new PagedResult<Shoe>();

        userRepository.GetAllAsync(Arg.Any<PagedAndSortedResultRequest>()).Returns(Result.Ok(mockedShoes));

        // Act
        var result = await shoesListQueryHandler.Handle(shoesListQuery, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(0);
    }
}