using BuildingLink.DriverManagement.Application.Drivers.Create;
using FluentAssertions;
using MediatR;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Create;

public class CreateDriverCommandSpecifications
{
    [Fact]
    public void CreateDriverCommand_AttemptToCreateObject_ShouldSucceed()
    {
        var command = new CreateDriverCommand
        {
            Email = "test@test.com",
            FirstName = "MyFirstName",
            LastName = "MyLastName",
            PhoneNumber = "+1-404-724-1937"
        };

        command.Should().NotBeNull();
    }

    [Fact]
    public void CreateDriverCommand_AttemptToCreateObject_ShouldHaveFirstNameLastNameEmailPhoneNumber()
    {
        var command = new CreateDriverCommand
        {
            Email = "test@test.com",
            FirstName = "MyFirstName",
            LastName = "MyLastName",
            PhoneNumber = "+1-404-724-1937"
        };

        command.Should().NotBeNull();
        command.FirstName.Should().NotBeNull();
        command.LastName.Should().NotBeNull();
        command.Email.Should().NotBeNull();
        command.PhoneNumber.Should().NotBeNull();
    }

    [Fact]
    public void CreateDriverCommand_CreateObject_ShouldBeOfTypeIRequestCreateDriverCommandResponse()
    {
        typeof(CreateDriverCommand).Should().Implement<IRequest<CreateDriverCommandResponse>>();
    }
}