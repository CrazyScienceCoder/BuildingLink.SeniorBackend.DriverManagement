using BuildingLink.DriverManagement.Application.Drivers.Delete;
using FluentAssertions;
using MediatR;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Delete;

public class DeleteDriverCommandSpecifications
{
    [Fact]
    public void DeleteDriverCommand_AttemptToCreateObject_ShouldSucceed()
    {
        var query = new DeleteDriverCommand
        {
            DriverId = 1
        };

        query.Should().NotBeNull();
    }

    [Fact]
    public void DeleteDriverCommand_AttemptToCreateObject_ShouldHaveDriverId()
    {
        var query = new DeleteDriverCommand
        {
            DriverId = 1
        };

        query.Should().NotBeNull();
        query.DriverId.Should().Be(1);
    }

    [Fact]
    public void DeleteDriverCommand_CreateObject_ShouldBeOfTypeIRequestDeleteDriverCommandResponse()
    {
        typeof(DeleteDriverCommand).Should().Implement<IRequest<DeleteDriverCommandResponse>>();
    }
}