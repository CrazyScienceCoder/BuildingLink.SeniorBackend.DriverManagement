using System.Net;
using BuildingLink.DriverManagement.Application.Drivers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink.DriverManagement.WebApi.Tests.Features.Drivers.Update;

public partial class UpdateDriverEndpointSpecifications
{
    [Fact]
    public async Task InvokeAsync_ValidRequest_ReturnsCreatedResponse()
    {
        var testBuilder = new TestBuilder()
            .SetupSuccess();

        var controller = testBuilder.Build();

        var result = await controller.InvokeAsync(TestBuilder.DefaultId, TestBuilder.DefaultRequest, CancellationToken.None);

        testBuilder.MediatorMock.Verify();
        result.Should().NotBeNull();

        var objectResult = result as ObjectResult;

        objectResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
        objectResult.Value.Should().NotBeNull();
        objectResult.Value.Should().BeOfType<DriverResult>();
    }

    [Fact]
    public async Task InvokeAsync_DomainValidationError_ReturnsBadRequest()
    {
        var testBuilder = new TestBuilder()
            .SetupValidationError();

        var controller = testBuilder.Build();

        var result = await controller.InvokeAsync(TestBuilder.DefaultId, TestBuilder.DefaultRequest, CancellationToken.None);

        testBuilder.MediatorMock.Verify();
        result.Should().NotBeNull();

        var objectResult = result as ObjectResult;

        objectResult!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        objectResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task InvokeAsync_NotFoundError_ReturnsNotFound()
    {
        var testBuilder = new TestBuilder()
            .SetupNotFoundError();

        var controller = testBuilder.Build();

        var result = await controller.InvokeAsync(TestBuilder.DefaultId, TestBuilder.DefaultRequest, CancellationToken.None);

        testBuilder.MediatorMock.Verify();
        result.Should().NotBeNull();

        var objectResult = result as ObjectResult;

        objectResult!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        objectResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task InvokeAsync_GenericError_ReturnsBadRequest()
    {
        var testBuilder = new TestBuilder()
            .SetupGenericError();

        var controller = testBuilder.Build();

        var result = await controller.InvokeAsync(TestBuilder.DefaultId, TestBuilder.DefaultRequest, CancellationToken.None);

        testBuilder.MediatorMock.Verify();
        result.Should().NotBeNull();

        var objectResult = result as ObjectResult;

        objectResult!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        objectResult.Value.Should().NotBeNull();
    }
}