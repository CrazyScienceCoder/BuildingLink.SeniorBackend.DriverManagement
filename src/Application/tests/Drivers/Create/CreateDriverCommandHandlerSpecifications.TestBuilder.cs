using BuildingLink.DriverManagement.Application.Drivers.Create;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Create;

public partial class CreateDriverCommandHandlerSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<IDriverRepository> DriverRepositoryMock = new();
        public readonly Mock<ILogger<CreateDriverCommandHandler>> LoggerMock = new();

        public static CreateDriverCommand DefaultCommand => new()
        {
            Email = "test@test.com",
            FirstName = "MyFirstName",
            LastName = "MyLastName",
            PhoneNumber = "+1-404-724-1937"
        };

        public TestBuilder SetupDriverRepository(int driverId = 1)
        {
            DriverRepositoryMock.Setup(s => s.CreateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(driverId);

            return this;
        }

        public TestBuilder SetupDriverRepositoryException()
        {
            DriverRepositoryMock.Setup(s => s.CreateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test Exception"));

            return this;
        }

        public TestBuilder SetupDriverRepositoryDomainValidationException()
        {
            DriverRepositoryMock.Setup(s => s.CreateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidEmailException("Test Exception", "email"));

            return this;
        }

        public CreateDriverCommandHandler Build()
        {
            return new CreateDriverCommandHandler(DriverRepositoryMock.Object, LoggerMock.Object);
        }
    }
}