using BuildingLink.DriverManagement.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.WebApi.Tests.Middlewares;

public partial class RequestLoggingMiddlewareSpecifications
{
    private class TestBuilder
    {
        public readonly Mock<ILogger<RequestLoggingMiddleware>> LoggerMock = new();
        private readonly Mock<RequestDelegate> _nextMock = new();

        public TestBuilder SetupRequestDelegate(HttpContext context)
        {
            _nextMock.Setup(next => next(context)).Returns(Task.CompletedTask);

            return this;
        }

        public RequestLoggingMiddleware Build()
        {
            return new RequestLoggingMiddleware(_nextMock.Object, LoggerMock.Object);
        }
    }
}