using BuildingLink.DriverManagement.Domain.Drivers;

namespace BuildingLink.DriverManagement.Application.Drivers.Update.Mappers;

public static class UpdateDriverCommandMapper
{
    public static Driver ToDomainDriver(this UpdateDriverCommand command)
    {
        return new Driver
        {
            Id = command.Id,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PhoneNumber = command.PhoneNumber
        };
    }
}