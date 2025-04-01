using BuildingLink.DriverManagement.Application.Drivers.Update;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Update;

public partial class UpdateDriverCommandHandlerSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<IDriverRepository> DriverRepositoryMock = new();
        public readonly Mock<ILogger<UpdateDriverCommandHandler>> LoggerMock = new();

        public static UpdateDriverCommand DefaultCommand => new()
        {
            Id = 1,
            Email = "test@test.com",
            FirstName = "MyFirstName",
            LastName = "MyLastName",
            PhoneNumber = "+1-404-724-1937"
        };

        public TestBuilder SetupDriverRepository(bool isDeleteSucceeded = true, bool exists = true)
        {
            DriverRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(isDeleteSucceeded);

            DriverRepositoryMock.Setup(s => s.ExistsAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(exists);

            return this;
        }

        public TestBuilder SetupDriverRepositoryException()
        {
            DriverRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test Exception"));

            return this;
        }

        public TestBuilder SetupDriverRepositoryDomainValidationException()
        {
            DriverRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidEmailException("Test Exception", "email"));

            return this;
        }

        public UpdateDriverCommandHandler Build()
        {
            return new UpdateDriverCommandHandler(DriverRepositoryMock.Object, LoggerMock.Object);
        }
    }
}