using System.ComponentModel.DataAnnotations;
using BuildingLink.DriverManagement.Application.Drivers.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink.DriverManagement.WebApi.Features.Drivers.Delete;

[Route(Routes.DriversV1Path)]
[ApiController]
public class DeleteDriverEndpoint(IMediator mediator) : WebApiBaseController
{
    [HttpDelete("{driverId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InvokeAsync([FromRoute, Range(1, int.MaxValue)] int driverId, CancellationToken cancellationToken)
    {
        var command = new DeleteDriverCommand
        {
            DriverId = driverId
        };

        var result = await mediator.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BuildFailureResponse(result);
    }
}