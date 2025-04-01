namespace BuildingLink.DriverManagement.Application.Drivers;

public class DriverResult
{
    public required int Id { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }

    public required string PhoneNumber { get; init; }

    public required string FullName { get; set; }

    public required string AlphabetizedFullName { get; set; }
}