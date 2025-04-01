using BuildingLink.DriverManagement.Application.Drivers;
using BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;
using BuildingLink.DriverManagement.Application.Shared;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.GetAlphabetizedCollection;

public class GetAlphabetizedCollectionQueryResponseSpecifications
{
    [Fact]
    public void GetAlphabetizedCollectionQueryResponse_AttemptToCreateSuccessObject_ShouldSucceed()
    {
        var driverResult = new List<DriverResult>
        {
            new()
            {
                Id = 1,
                Email = "test@domain.com",
                FirstName = "Oliver",
                LastName = "Johnson",
                PhoneNumber = "+1-404-724-1937",
                FullName = "Oliver Johnson",
                AlphabetizedFullName = "eilOrv hJnnoos"
            },
            new()
            {
                Id = 2,
                Email = "test2@domain.com",
                FirstName = "Dcba",
                LastName = "Fehg",
                PhoneNumber = "+1-404-724-1938",
                FullName = "Dcba Fehg",
                AlphabetizedFullName = "abcD eFgh"
            }
        };

        var response = GetAlphabetizedCollectionQueryResponse.Success(driverResult, "Test message");

        response.Data.Should().NotBeNull();
        response.Data.Should().HaveCount(2);
        response.Data.Should().BeEquivalentTo(driverResult);
        response.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void GetAlphabetizedCollectionQueryResponse_AttemptToCreateFailureObject_ShouldSucceed()
    {
        var response = GetAlphabetizedCollectionQueryResponse.Failure(exception: new Exception("Test"), errorType: ErrorType.Generic,message: "Test message");

        response.Error.Should().NotBeNull();
        response.IsSuccess.Should().BeFalse();
    }
}