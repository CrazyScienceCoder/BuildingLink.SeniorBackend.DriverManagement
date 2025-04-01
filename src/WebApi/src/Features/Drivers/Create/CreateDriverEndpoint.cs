using BuildingLink.DriverManagement.Application.Drivers.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink.DriverManagement.WebApi.Features.Drivers.Create;

[Route(Routes.DriversV1Path)]
[ApiController]
public class CreateDriverEndpoint(IMediator mediator) : WebApiBaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InvokeAsync([FromBody] CreateDriverRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateDriverCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        var result = await mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BuildFailureResponse(result);
        }

        var url = Url.Action(action: "Invoke", controller: "GetDriverEndpoint", values: new { driverId = result.Data!.Id });

        return Created(url, result.Data);
    }
}