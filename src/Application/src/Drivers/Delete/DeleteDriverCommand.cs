using MediatR;

namespace BuildingLink.DriverManagement.Application.Drivers.Delete;

public sealed class DeleteDriverCommand : IRequest<DeleteDriverCommandResponse>
{
    public required int DriverId { get; set; }
}