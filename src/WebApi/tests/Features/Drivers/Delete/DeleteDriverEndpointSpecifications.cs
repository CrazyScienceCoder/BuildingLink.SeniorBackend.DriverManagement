using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink.DriverManagement.WebApi.Tests.Features.Drivers.Delete;

public partial class DeleteDriverEndpointSpecifications
{
    [Fact]
    public async Task InvokeAsync_ValidRequest_ReturnsCreatedResponse()
    {
        var testBuilder = new TestBuilder()
            .SetupSuccess();

        var request = TestBuilder.DefaultRequest;

        var controller = testBuilder.Build();

        var result = await controller.InvokeAsync(request, CancellationToken.None);

        testBuilder.MediatorMock.Verify();
        result.Should().NotBeNull();

        var objectResult = result as NoContentResult;

        objectResult!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task InvokeAsync_DomainValidationError_ReturnsBadRequest()
    {
        var testBuilder = new TestBuilder()
            .SetupValidationError();

        var request = TestBuilder.DefaultRequest;

        var controller = testBuilder.Build();

        var result = await controller.InvokeAsync(request, CancellationToken.None);

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

        var request = TestBuilder.DefaultRequest;

        var controller = testBuilder.Build();

        var result = await controller.InvokeAsync(request, CancellationToken.None);

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

        var request = TestBuilder.DefaultRequest;

        var controller = testBuilder.Build();

        var result = await controller.InvokeAsync(request, CancellationToken.None);

        testBuilder.MediatorMock.Verify();
        result.Should().NotBeNull();

        var objectResult = result as ObjectResult;

        objectResult!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        objectResult.Value.Should().NotBeNull();
    }
}