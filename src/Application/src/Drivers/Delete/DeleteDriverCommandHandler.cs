using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Domain.Drivers;
using Microsoft.Extensions.Logging;

namespace BuildingLink.DriverManagement.Application.Drivers.Delete;

public sealed class DeleteDriverCommandHandler(IDriverRepository driverRepository, ILogger<DeleteDriverCommandHandler> logger)
    : HandlerBase<DeleteDriverCommand, DeleteDriverCommandResponse, DriverResult, Driver>(driverRepository, logger)
{
    protected override async Task<DeleteDriverCommandResponse> ExecuteAsync(DeleteDriverCommand request, CancellationToken cancellationToken)
    {
        var isSuccess = await driverRepository.DeleteAsync(request.DriverId, cancellationToken);

        if (!isSuccess)
        {
            return await BuildFailureResponseAsync(request.DriverId, cancellationToken);
        }

        return DeleteDriverCommandResponse.Success(message: "Driver was deleted successfully");
    }
}