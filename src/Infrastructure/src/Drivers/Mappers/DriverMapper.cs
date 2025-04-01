using BuildingLink.DriverManagement.Domain.Drivers;

namespace BuildingLink.DriverManagement.Infrastructure.Drivers.Mappers;

public static class DriverMapper
{
    public static Models.Driver ToDriverModel(this Driver driver)
    {
        return new Models.Driver
        {
            Id = driver.Id,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Email = driver.Email,
            PhoneNumber = driver.PhoneNumber
        };
    }

    public static Driver ToDomainDriver(this Models.Driver driver)
    {
        return new Driver
        {
            Id = driver.Id,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Email = driver.Email,
            PhoneNumber = driver.PhoneNumber
        };
    }

    public static IEnumerable<Driver> ToDomainDrivers(this IEnumerable<Models.Driver> drivers)
    {
        return drivers.Select(d => d.ToDomainDriver());
    }
}