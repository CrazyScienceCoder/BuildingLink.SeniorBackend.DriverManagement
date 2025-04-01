using BuildingLink.DriverManagement.Application.Drivers.Get;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Get;

public partial class GetDriverQueryHandlerSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<IDriverRepository> DriverRepositoryMock = new();
        public readonly Mock<ILogger<GetDriverQueryHandler>> LoggerMock = new();

        public static GetDriverQuery DefaultDriverQuery => new()
        {
            DriverId = 1
        };

        public TestBuilder SetupDriverRepository()
        {
            var driver = new Driver
            {
                Id = 1,
                Email = "test@domain.com",
                FirstName = "Oliver",
                LastName = "Johnson",
                PhoneNumber = "+1-404-724-1937"
            };

            DriverRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(driver);

            return this;
        }

        public TestBuilder SetupEmptyDriverRepository()
        {
            DriverRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Driver?)null);

            return this;
        }

        public TestBuilder SetupDriverRepositoryException()
        {
            DriverRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test Exception"));

            return this;
        }

        public TestBuilder SetupDriverRepositoryDomainValidationException()
        {
            DriverRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidEmailException("Test Exception", "email"));

            return this;
        }

        public GetDriverQueryHandler Build()
        {
            return new GetDriverQueryHandler(DriverRepositoryMock.Object, LoggerMock.Object);
        }
    }
}