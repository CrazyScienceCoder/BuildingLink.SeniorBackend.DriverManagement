using MediatR;

namespace BuildingLink.DriverManagement.Application.Drivers.Create;

public class CreateDriverCommand : IRequest<CreateDriverCommandResponse>
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }
}