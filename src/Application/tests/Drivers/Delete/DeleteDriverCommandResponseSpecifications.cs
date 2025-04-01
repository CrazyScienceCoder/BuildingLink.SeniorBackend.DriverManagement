using BuildingLink.DriverManagement.Application.Drivers.Delete;
using BuildingLink.DriverManagement.Application.Shared;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Delete;

public class DeleteDriverCommandResponseSpecifications
{
    [Fact]
    public void DeleteDriverCommandResponse_AttemptToCreateSuccessObject_ShouldSucceed()
    {
        var response = DeleteDriverCommandResponse.Success(message: "Test message");

        response.Data.Should().BeNull();
        response.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void DeleteDriverCommandResponse_AttemptToCreateFailureObject_ShouldSucceed()
    {
        var response = DeleteDriverCommandResponse.Failure(exception: new Exception("Test"),errorType: ErrorType.Generic, message: "Test message");

        response.Error.Should().NotBeNull();
        response.IsSuccess.Should().BeFalse();
    }
}