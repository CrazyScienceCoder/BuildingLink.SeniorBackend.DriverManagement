using BuildingLink.DriverManagement.Application.Drivers;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Types;

namespace BuildingLink.DriverManagement.Application.Shared.Mappers;

public static class DriverMapper
{
    public static DriverResult ToDriverResult(this Driver driver, Id? id = null)
    {
        return new DriverResult
        {
            Id = id ?? driver.Id,
            Email = driver.Email,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            PhoneNumber = driver.PhoneNumber,
            FullName = $"{driver.FirstName.Value} {driver.LastName.Value}",
            AlphabetizedFullName = $"{driver.FirstName.AlphabetizedValue} {driver.LastName.AlphabetizedValue}"
        };
    }

    public static IEnumerable<DriverResult> ToDriverResult(this IEnumerable<Driver> drivers)
    {
        return drivers.Select(d => d.ToDriverResult());
    }
}