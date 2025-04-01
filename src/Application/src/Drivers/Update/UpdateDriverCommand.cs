using MediatR;

namespace BuildingLink.DriverManagement.Application.Drivers.Update;

public class UpdateDriverCommand : IRequest<UpdateDriverCommandResponse>
{
    public required int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }
}