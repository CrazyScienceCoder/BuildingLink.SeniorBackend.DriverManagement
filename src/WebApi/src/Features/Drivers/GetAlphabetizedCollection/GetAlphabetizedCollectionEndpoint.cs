using BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink.DriverManagement.WebApi.Features.Drivers.GetAlphabetizedCollection;

[Route(Routes.DriversV1Path)]
[ApiController]
public sealed class GetAlphabetizedCollectionEndpoint(IMediator mediator) : WebApiBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InvokeAsync([FromQuery] GetAlphabetizedCollectionRequest request, CancellationToken cancellationToken)
    {
        var query = new GetAlphabetizedCollectionQuery
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Data) : BuildFailureResponse(result);
    }
}