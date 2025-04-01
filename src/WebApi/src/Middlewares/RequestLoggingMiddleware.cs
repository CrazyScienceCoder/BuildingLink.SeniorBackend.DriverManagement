using System.Text;

namespace BuildingLink.DriverManagement.WebApi.Middlewares;

public sealed class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();

        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            logger.LogInformation("Request {Method} {Path} Body: {Body}",
                context.Request.Method,
                context.Request.Path,
                body);
        }

        await next(context);
    }
}
