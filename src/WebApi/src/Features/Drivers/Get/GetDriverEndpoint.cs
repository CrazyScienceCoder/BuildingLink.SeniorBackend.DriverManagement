using System.ComponentModel.DataAnnotations;
using BuildingLink.DriverManagement.Application.Drivers.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink.DriverManagement.WebApi.Features.Drivers.Get;

[Route(Routes.DriversV1Path)]
[ApiController]
public class GetDriverEndpoint(IMediator mediator) : WebApiBaseController
{
    [HttpGet("{driverId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InvokeAsync([FromRoute, Range(1, int.MaxValue)] int driverId, CancellationToken cancellationToken)
    {
        var query = new GetDriverQuery
        {
            DriverId = driverId
        };

        var result = await mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Data) : BuildFailureResponse(result);
    }
}