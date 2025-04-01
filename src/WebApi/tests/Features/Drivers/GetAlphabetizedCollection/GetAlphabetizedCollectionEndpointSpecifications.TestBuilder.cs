using BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;
using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Application.Shared.Mappers;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.WebApi.Features.Drivers.GetAlphabetizedCollection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace BuildingLink.DriverManagement.WebApi.Tests.Features.Drivers.GetAlphabetizedCollection;

public partial class GetAlphabetizedCollectionEndpointSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<IMediator> MediatorMock = new();

        public static readonly GetAlphabetizedCollectionRequest DefaultRequest = new()
        {
            PageNumber = 1,
            PageSize = 1
        };

        public TestBuilder SetupSuccess()
        {
            List<Driver> drivers =
            [
                new()
                {
                    Id = 1,
                    Email = "test@domain.com",
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    PhoneNumber = "+13213213213"
                }
            ];

            MediatorMock.Setup(s => s.Send(It.IsAny<GetAlphabetizedCollectionQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetAlphabetizedCollectionQueryResponse.Success(drivers.ToDriverResult().ToList()))
                .Verifiable(Times.Once);

            return this;
        }

        public TestBuilder SetupGenericError()
        {
            MediatorMock.Setup(s => s.Send(It.IsAny<GetAlphabetizedCollectionQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    GetAlphabetizedCollectionQueryResponse.Failure(errorType: ErrorType.Generic, message: "Testing error"))
                .Verifiable(Times.Once);

            return this;
        }

        public GetAlphabetizedCollectionEndpoint Build()
        {
            var endpoint = new GetAlphabetizedCollectionEndpoint(MediatorMock.Object)
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