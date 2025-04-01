using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Application.Shared.Mappers;
using BuildingLink.DriverManagement.Domain.Drivers;
using Microsoft.Extensions.Logging;

namespace BuildingLink.DriverManagement.Application.Drivers.Get;

public class GetDriverQueryHandler(IDriverRepository driverRepository, ILogger<GetDriverQueryHandler> logger)
    : HandlerBase<GetDriverQuery, GetDriverQueryResponse, DriverResult, Driver>(driverRepository, logger)
{
    protected override async Task<GetDriverQueryResponse> ExecuteAsync(GetDriverQuery request, CancellationToken cancellationToken)
    {
        var driver = await driverRepository.GetAsync(request.DriverId, cancellationToken);

        if (driver == null)
        {
            return GetDriverQueryResponse.Failure(errorType: ErrorType.RecordNotFound,
                message: $"Driver was not found, Id: {request.DriverId}");
        }

        return GetDriverQueryResponse.Success(driver?.ToDriverResult(), "Driver was retrieved successfully");
    }
}