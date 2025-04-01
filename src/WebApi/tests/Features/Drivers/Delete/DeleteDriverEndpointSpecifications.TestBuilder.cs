using BuildingLink.DriverManagement.Application.Drivers.Delete;
using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.WebApi.Features.Drivers.Delete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace BuildingLink.DriverManagement.WebApi.Tests.Features.Drivers.Delete;

public partial class DeleteDriverEndpointSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<IMediator> MediatorMock = new();

        public const int DefaultRequest = 1;

        public TestBuilder SetupSuccess()
        {
            MediatorMock.Setup(s => s.Send(It.IsAny<DeleteDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(DeleteDriverCommandResponse.Success(message: "Driver was deleted successfully"))
                .Verifiable(Times.Once);

            return this;
        }

        public TestBuilder SetupValidationError()
        {
            MediatorMock.Setup(s => s.Send(It.IsAny<DeleteDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(DeleteDriverCommandResponse.Failure(errorType: ErrorType.ValidationError,
                    exception: new InvalidEmailException("test invalid input", "input")))
                .Verifiable(Times.Once);

            return this;
        }

        public TestBuilder SetupNotFoundError()
        {
            MediatorMock.Setup(s => s.Send(It.IsAny<DeleteDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(DeleteDriverCommandResponse.Failure(errorType: ErrorType.RecordNotFound, message:"Record not found"))
                .Verifiable(Times.Once);

            return this;
        }

        public TestBuilder SetupGenericError()
        {
            MediatorMock.Setup(s => s.Send(It.IsAny<DeleteDriverCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    DeleteDriverCommandResponse.Failure(errorType: ErrorType.Generic, message: "Testing error"))
                .Verifiable(Times.Once);

            return this;
        }

        public DeleteDriverEndpoint Build()
        {
            var endpoint = new DeleteDriverEndpoint(MediatorMock.Object)
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