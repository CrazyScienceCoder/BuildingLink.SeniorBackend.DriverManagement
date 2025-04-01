using MediatR;

namespace BuildingLink.DriverManagement.Application.Drivers.Delete;

public class DeleteDriverCommand : IRequest<DeleteDriverCommandResponse>
{
    public required int DriverId { get; set; }
}