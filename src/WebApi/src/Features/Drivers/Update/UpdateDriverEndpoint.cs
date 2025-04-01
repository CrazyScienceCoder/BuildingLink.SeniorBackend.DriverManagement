using System.ComponentModel.DataAnnotations;
using BuildingLink.DriverManagement.Application.Drivers.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink.DriverManagement.WebApi.Features.Drivers.Update;

[Route(Routes.DriversV1Path)]
[ApiController]
public sealed class UpdateDriverEndpoint(IMediator mediator) : WebApiBaseController
{
    [HttpPut("{driverId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InvokeAsync([FromRoute, Range(1, int.MaxValue)] int driverId, [FromBody] UpdateDriverRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateDriverCommand
        {
            Id = driverId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        var result = await mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Data) : BuildFailureResponse(result);
    }
}