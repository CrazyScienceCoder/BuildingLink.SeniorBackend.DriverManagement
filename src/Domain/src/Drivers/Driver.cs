using BuildingLink.DriverManagement.Domain.Types;

namespace BuildingLink.DriverManagement.Domain.Drivers;

public record Driver
{
    public required Id Id { get; set; }

    public required Name FirstName { get; set; }

    public required Name LastName { get; set; }

    public required Email Email { get; set; }

    public required PhoneNumber PhoneNumber { get; set; }
}