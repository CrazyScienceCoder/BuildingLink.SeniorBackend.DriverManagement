using MediatR;

namespace BuildingLink.DriverManagement.Application.Drivers.Get;

public class GetDriverQuery : IRequest<GetDriverQueryResponse>
{
    public required int DriverId { get; set; }
}