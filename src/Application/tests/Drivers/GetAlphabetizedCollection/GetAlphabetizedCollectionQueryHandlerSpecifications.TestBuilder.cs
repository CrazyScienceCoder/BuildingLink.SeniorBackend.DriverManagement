using BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.GetAlphabetizedCollection;

public partial class GetAlphabetizedCollectionQueryHandlerSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<IDriverRepository> DriverRepositoryMock = new();
        public readonly Mock<ILogger<GetAlphabetizedCollectionQueryHandler>> LoggerMock = new();

        public static GetAlphabetizedCollectionQuery DefaultDriverQuery => new();

        public TestBuilder SetupDriverRepository()
        {
            var driverResult = new List<Driver>
            {
                new()
                {
                    Id = 1,
                    Email = "test@domain.com",
                    FirstName = "Oliver",
                    LastName = "Johnson",
                    PhoneNumber = "+1-404-724-1937"
                },
                new()
                {
                    Id = 2,
                    Email = "test2@domain.com",
                    FirstName = "Dcba",
                    LastName = "Fehg",
                    PhoneNumber = "+1-404-724-1938"
                }
            };

            DriverRepositoryMock.Setup(s =>
                    s.GetAlphabetizedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(driverResult);

            return this;
        }

        public TestBuilder SetupDriverRepositoryException()
        {
            DriverRepositoryMock.Setup(s => s.GetAlphabetizedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test Exception"));

            return this;
        }

        public TestBuilder SetupDriverRepositoryDomainValidationException()
        {
            DriverRepositoryMock.Setup(s => s.GetAlphabetizedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidEmailException("Test Exception", "email"));

            return this;
        }

        public GetAlphabetizedCollectionQueryHandler Build()
        {
            return new GetAlphabetizedCollectionQueryHandler(DriverRepositoryMock.Object, LoggerMock.Object);
        }
    }
}