using BuildingLink.DriverManagement.Application.Drivers.Update.Mappers;
using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Application.Shared.Mappers;
using BuildingLink.DriverManagement.Domain.Drivers;
using Microsoft.Extensions.Logging;

namespace BuildingLink.DriverManagement.Application.Drivers.Update;

public class UpdateDriverCommandHandler(IDriverRepository driverRepository, ILogger<UpdateDriverCommandHandler> logger)
    : HandlerBase<UpdateDriverCommand, UpdateDriverCommandResponse, DriverResult, Driver>(driverRepository, logger)
{
    protected override async Task<UpdateDriverCommandResponse> ExecuteAsync(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = request.ToDomainDriver();

        var isSuccess = await driverRepository.UpdateAsync(driver, cancellationToken);

        if (!isSuccess)
        {
            return await BuildFailureResponseAsync(request.Id, cancellationToken);
        }

        return UpdateDriverCommandResponse.Success(driver.ToDriverResult(), "Driver was updated successfully");
    }
}