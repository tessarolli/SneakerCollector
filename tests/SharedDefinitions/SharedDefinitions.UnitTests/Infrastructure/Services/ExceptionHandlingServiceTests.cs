using SharedDefinitions.Infrastructure.Services;
using System.Net.Sockets;

namespace SharedDefinitions.UnitTests.Infrastructure.Services;

public class ExceptionHandlingServiceTests
{
    [Fact]
    public void HandleException_WithSocketException_ReturnsNetworkConnectivityError()
    {
        // Arrange
        var service = new ExceptionHandlingService();
        var socketException = new SocketException();

        // Act
        var result = service.HandleException(socketException);

        // Assert
        result.Errors.Should().ContainSingle()
        .Which.Message.Should().Be("An unexpected error occurred on our end. We are looking into it. In the mean time, try the request again.");
    }


    [Fact]
    public void HandleException_WithInvalidOperationException_ReturnsGenericError()
    {
        // Arrange
        var service = new ExceptionHandlingService();
        var invalidOperationException = new InvalidOperationException();

        // Act
        var result = service.HandleException(invalidOperationException);

        // Assert
        result.Errors.Should().ContainSingle()
        .Which.Message.Should().Be("The operation is invalid.");
    }

    [Fact]
    public void HandleException_WithNoInnerException_ReturnsUnexpectedError()
    {
        // Arrange
        var service = new ExceptionHandlingService();
        var exception = new Exception();

        // Act
        var result = service.HandleException(exception);

        // Assert
        result.Errors.Should().ContainSingle()
        .Which.Message.Should().Be("An unexpected error occurred on our end. We are looking into it. In the mean time, try the request again.");
    }
}