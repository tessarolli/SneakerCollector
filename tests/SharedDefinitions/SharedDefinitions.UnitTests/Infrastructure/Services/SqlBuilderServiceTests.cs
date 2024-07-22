using SharedDefinitions.Infrastructure.Services;
using SharedDefinitions.Application.Models;

namespace SharedDefinitions.UnitTests.Infrastructure.Services;

public class SqlBuilderServiceTests
{
    private readonly SqlBuilderService _sut;

    public SqlBuilderServiceTests()
    {
        _sut = new SqlBuilderService();
    }

    [Fact]
    public void BuildPagedQuery_WithValidInput_ShouldReturnSuccessResult()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 10,
            Search = "Nike",
            Sort = "Price"
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.querySql.Should().Contain("SELECT Id, Name, Price FROM Products");
        result.Value.querySql.Should().Contain("WHERE (LOWER(Name) LIKE LOWER(@Search) OR LOWER(Description) LIKE LOWER(@Search))");
        result.Value.querySql.Should().Contain("ORDER BY Price ASC");
        result.Value.querySql.Should().Contain("OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY");
        result.Value.counterSql.Should().Contain("SELECT COUNT(*) FROM Products");
        result.Value.parameters.Should().ContainKeys("Offset", "Limit", "Search");
        result.Value.parameters["Search"].Should().Be("%Nike%");
    }

    [Fact]
    public void BuildPagedQuery_WithInvalidSortField_ShouldReturnFailureResult()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 10,
            Sort = "InvalidField"
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("Invalid sort field: InvalidField");
    }

    [Fact]
    public void BuildPagedQuery_WithoutSort_ShouldUseDefaultOrderBy()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 10
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.querySql.Should().Contain("ORDER BY Id ASC");
    }

    [Fact]
    public void BuildPagedQuery_WithDescendingSort_ShouldOrderDescending()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 10,
            Sort = "-Price"
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.querySql.Should().Contain("ORDER BY Price DESC");
    }

    [Fact]
    public void BuildPagedQuery_WithEmptyFields_ShouldReturnFailureResult()
    {
        // Arrange
        var fields = "";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 10
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("Fields parameter cannot be empty");
    }

    [Fact]
    public void BuildPagedQuery_WithEmptyTables_ShouldReturnFailureResult()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 10
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("Tables parameter cannot be empty");
    }

    [Fact]
    public void BuildPagedQuery_WithNegativeOffset_ShouldReturnFailureResult()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = -1,
            Limit = 10
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("Offset must be non-negative");
    }

    [Fact]
    public void BuildPagedQuery_WithZeroLimit_ShouldReturnFailureResult()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 0
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("Limit must be greater than zero");
    }

    [Fact]
    public void BuildPagedQuery_WithEmptyValidSortFields_ShouldUseDefaultOrderBy()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 10,
            Sort = "Price"
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>();

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.querySql.Should().Contain("ORDER BY Id ASC");
    }

    [Fact]
    public void BuildPagedQuery_WithSpecialCharactersInSearch_ShouldEscapeSpecialCharacters()
    {
        // Arrange
        var fields = "Id, Name, Price";
        var tables = "Products";
        var request = new PagedAndSortedResultRequest
        {
            Offset = 0,
            Limit = 10,
            Search = "Nike%_[]"
        };
        var searchColumns = "Name, Description";
        var defaultOrderBy = "Id ASC";
        var validSortFields = new Dictionary<string, string>
        {
            { "Price", "Price" },
            { "Name", "Name" }
        };

        // Act
        var result = _sut.BuildPagedQuery(fields, tables, request, searchColumns, defaultOrderBy, validSortFields);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.parameters["Search"].Should().Be("%Nike[%][_][[][]]%");
    }
}