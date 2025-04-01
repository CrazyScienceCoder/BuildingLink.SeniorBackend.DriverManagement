using BuildingLink.DriverManagement.Application.Drivers.Create.Mappers;
using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Application.Shared.Mappers;
using BuildingLink.DriverManagement.Domain.Drivers;
using Microsoft.Extensions.Logging;

namespace BuildingLink.DriverManagement.Application.Drivers.Create;

public class CreateDriverCommandHandler(IDriverRepository driverRepository, ILogger<CreateDriverCommandHandler> logger)
    : HandlerBase<CreateDriverCommand, CreateDriverCommandResponse, DriverResult, Driver>(driverRepository, logger)
{
    protected override async Task<CreateDriverCommandResponse> ExecuteAsync(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = request.ToDomainDriver();
        var driverId = await driverRepository.CreateAsync(driver, cancellationToken);

        return CreateDriverCommandResponse.Success(driver.ToDriverResult(driverId), "Driver was created successfully");
    }
}