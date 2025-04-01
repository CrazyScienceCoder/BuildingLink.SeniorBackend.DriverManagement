using System.Net;
using BuildingLink.DriverManagement.WebApi.Filters;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace BuildingLink.DriverManagement.WebApi.Tests.Filters;

public class ExceptionFilterSpecifications
{
    [Fact]
    public void ExceptionFilter_OnException_ShouldReturnInternalServerError()
    {
        var loggerMock = new Mock<ILogger<ExceptionFilter>>();
        var filter = new ExceptionFilter(loggerMock.Object);

        var exception = new Exception("Test exception");
        var actionContext = new ActionContext(
            new DefaultHttpContext(),
            new RouteData(),
            new ActionDescriptor()
        );

        var exceptionContext = new ExceptionContext(actionContext, new Mock<IList<IFilterMetadata>>().Object)
        {
            Exception = exception
        };

        var expectedResponse = new
        {
            Error = "An unexpected error occurred.",
            Details = exceptionContext.Exception.Message
        };

        filter.OnException(exceptionContext);

        exceptionContext.Result.Should().BeOfType<ObjectResult>();

        var objectResult = exceptionContext.Result as ObjectResult;

        objectResult!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

        objectResult.Value.Should().NotBeNull();

        JsonConvert.SerializeObject(expectedResponse).Should().Be(JsonConvert.SerializeObject(objectResult.Value));
    }
}