using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.WebApi.Tests.Middlewares;

public partial class RequestLoggingMiddlewareSpecifications
{
    [Fact]
    public async Task Invoke_WithRequestBody_LogsRequestBody()
    {
        const string requestBody = "test request body";
        var context = new DefaultHttpContext();
        var requestStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
        context.Request.Body = requestStream;
        context.Request.Method = "POST";
        context.Request.Path = "/test";

        var testBuilder = new TestBuilder()
            .SetupRequestDelegate(context);

        var middleware = testBuilder.Build();

        await middleware.Invoke(context);

        testBuilder.LoggerMock.Verify(
            log => log.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains(requestBody) &&
                                              o.ToString()!.Contains("POST") &&
                                              o.ToString()!.Contains("/test")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);

        context.Request.Body.Position.Should().Be(0);
    }
}