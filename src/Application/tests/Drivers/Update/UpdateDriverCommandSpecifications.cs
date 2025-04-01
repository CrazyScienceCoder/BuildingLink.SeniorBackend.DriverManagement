using BuildingLink.DriverManagement.Application.Drivers.Update;
using FluentAssertions;
using MediatR;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Update;

public class UpdateDriverCommandSpecifications
{
    [Fact]
    public void UpdateDriverCommand_AttemptToCreateObject_ShouldSucceed()
    {
        var command = new UpdateDriverCommand
        {
            Id = 1,
            Email = "test@domain.com",
            FirstName = "Oliver",
            LastName = "Johnson",
            PhoneNumber = "+1-404-724-1937"
        };

        command.Should().NotBeNull();
    }

    [Fact]
    public void UpdateDriverCommand_AttemptToCreateObject_ShouldHaveIdFirstNameLastNameEmailPhoneNumber()
    {
        var command = new UpdateDriverCommand
        {
            Id = 1,
            Email = "test@domain.com",
            FirstName = "Oliver",
            LastName = "Johnson",
            PhoneNumber = "+1-404-724-1937"
        };

        command.Should().NotBeNull();
        command.Id.Should().Be(1);
        command.FirstName.Should().NotBeNull();
        command.LastName.Should().NotBeNull();
        command.Email.Should().NotBeNull();
        command.PhoneNumber.Should().NotBeNull();
    }

    [Fact]
    public void UpdateDriverCommand_CreateObject_ShouldBeOfTypeIRequestUpdateDriverCommandResponse()
    {
        typeof(UpdateDriverCommand).Should().Implement<IRequest<UpdateDriverCommandResponse>>();
    }
}