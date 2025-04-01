using BuildingLink.DriverManagement.Application.Drivers.Update;
using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Application.Shared.Mappers;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.WebApi.Features.Drivers.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace BuildingLink.DriverManagement.WebApi.Tests.Features.Drivers.Update;

public partial class UpdateDriverEndpointSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<IMediator> MediatorMock = new();

        public const int DefaultId = 1;
        public static readonly UpdateDriverRequest DefaultRequest = new()
        {
            Email = "test@domain.com",
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            PhoneNumber = "+13213213213"
        };

        public TestBuilder SetupSuccess()
        {
            var driver = new Driver
            {
                Id = 1,
                Email = "test@domain.com",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "+13213213213"
            };

            MediatorMock.Setup(s => s.Send(It.IsAny<UpdateDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(UpdateDriverCommandResponse.Success(driver.ToDriverResult()))
                .Verifiable(Times.Once);

            return this;
        }

        public TestBuilder SetupValidationError()
        {
            MediatorMock.Setup(s => s.Send(It.IsAny<UpdateDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(UpdateDriverCommandResponse.Failure(errorType: ErrorType.ValidationError,
                    exception: new InvalidEmailException("test invalid input", "input")))
                .Verifiable(Times.Once);

            return this;
        }

        public TestBuilder SetupNotFoundError()
        {
            MediatorMock.Setup(s => s.Send(It.IsAny<UpdateDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(UpdateDriverCommandResponse.Failure(errorType: ErrorType.RecordNotFound, message:"Record not found"))
                .Verifiable(Times.Once);

            return this;
        }

        public TestBuilder SetupGenericError()
        {
            MediatorMock.Setup(s => s.Send(It.IsAny<UpdateDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    UpdateDriverCommandResponse.Failure(errorType: ErrorType.Generic, message: "Testing error"))
                .Verifiable(Times.Once);

            return this;
        }

        public UpdateDriverEndpoint Build()
        {
            var endpoint = new UpdateDriverEndpoint(MediatorMock.Object)
            {
                Url = CreateUrlHelper()
            };

            return endpoint;
        }

        private static IUrlHelper CreateUrlHelper()
        {
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

            var urlHelperFactoryMock = new Mock<IUrlHelperFactory>();
            var urlHelperMock = new Mock<IUrlHelper>();

            urlHelperFactoryMock.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelperMock.Object);

            return urlHelperFactoryMock.Object.GetUrlHelper(actionContext);
        }
    }
}