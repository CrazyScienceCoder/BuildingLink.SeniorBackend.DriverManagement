using MediatR;

namespace BuildingLink.DriverManagement.Application.Drivers.Get;

public sealed class GetDriverQuery : IRequest<GetDriverQueryResponse>
{
    public required int DriverId { get; set; }
}