using BuildingLink.DriverManagement.Application.Drivers.Delete;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Delete;

public partial class DeleteDriverCommandHandlerSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<IDriverRepository> DriverRepositoryMock = new();
        public readonly Mock<ILogger<DeleteDriverCommandHandler>> LoggerMock = new();

        public static DeleteDriverCommand DefaultCommand => new()
        {
            DriverId = 1
        };

        public TestBuilder SetupDriverRepository(bool isDeleteSucceeded = true, bool exists = true)
        {
            DriverRepositoryMock.Setup(s => s.DeleteAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(isDeleteSucceeded);

            DriverRepositoryMock.Setup(s => s.ExistsAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(exists);

            return this;
        }

        public TestBuilder SetupDriverRepositoryException()
        {
            DriverRepositoryMock.Setup(s => s.DeleteAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test Exception"));

            return this;
        }

        public TestBuilder SetupDriverRepositoryDomainValidationException()
        {
            DriverRepositoryMock.Setup(s => s.DeleteAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidEmailException("Test Exception", "email"));

            return this;
        }

        public DeleteDriverCommandHandler Build()
        {
            return new DeleteDriverCommandHandler(DriverRepositoryMock.Object, LoggerMock.Object);
        }
    }
}