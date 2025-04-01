using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink.DriverManagement.WebApi.Features;

public abstract class WebApiBaseController : ControllerBase
{
    protected IActionResult BuildFailureResponse<TData, TResponse>(Result<TData, TResponse> result)
        where TResponse : Result<TData, TResponse>, new()
    {
        if (result.ErrorType == ErrorType.RecordNotFound)
        {
            return NotFound(result.Message);
        }

        if (result.ErrorType != ErrorType.ValidationError)
        {
            return Problem($"An unexpected error occurred. Message: {result.Message}");
        }

        if (result.Error is DomainValidationException exception)
        {
            return ValidationProblem(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { exception.ParamName!, [exception.Message] }
            }));
        }

        return ValidationProblem(result.Error?.Message);
    }
}