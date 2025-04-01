using BuildingLink.DriverManagement.Application.Drivers;
using BuildingLink.DriverManagement.Application.Drivers.Create;
using BuildingLink.DriverManagement.Application.Shared;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Create;

public class CreateDriverCommandResponseSpecifications
{
    [Fact]
    public void CreateDriverCommandResponse_AttemptToCreateSuccessObject_ShouldSucceed()
    {
        var driverResult = new DriverResult
        {
            Id = 1,
            Email = "test@domain.com",
            FirstName = "Oliver",
            LastName = "Johnson",
            PhoneNumber = "+1-404-724-1937",
            FullName = "Oliver Johnson",
            AlphabetizedFullName = "eilOrv hJnnoos"
        };

        CreateDriverCommandResponse.Success(driverResult, "Test message");

        var response = CreateDriverCommandResponse.Success(driverResult, "Test message");

        response.Data.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void CreateDriverCommandResponse_AttemptToCreateFailureObject_ShouldSucceed()
    {
        var response = CreateDriverCommandResponse.Failure(exception: new Exception("Test"), errorType: ErrorType.Generic,message: "Test message");

        response.Error.Should().NotBeNull();
        response.IsSuccess.Should().BeFalse();
    }
}