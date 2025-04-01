using BuildingLink.DriverManagement.Domain.Drivers;

namespace BuildingLink.DriverManagement.Application.Drivers.Create.Mappers;

public static class CreateDriverCommandMapper
{
    public static Driver ToDomainDriver(this CreateDriverCommand command)
    {
        return new Driver
        {
            Id = 0,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PhoneNumber = command.PhoneNumber
        };
    }
}