namespace BuildingLink.DriverManagement.WebApi.Features.Drivers.Create;

public class CreateDriverRequest
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
}