using BuildingLink.DriverManagement.Application.Drivers.Get;
using FluentAssertions;
using MediatR;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Get;

public class GetDriverQuerySpecifications
{
    [Fact]
    public void GetDriverQuery_AttemptToCreateObject_ShouldSucceed()
    {
        var query = new GetDriverQuery
        {
            DriverId = 0
        };

        query.Should().NotBeNull();
    }

    [Fact]
    public void GetDriverQuery_AttemptToCreateObject_ShouldHaveDriverId()
    {
        var query = new GetDriverQuery
        {
            DriverId = 1
        };

        query.Should().NotBeNull();
        query.DriverId.Should().Be(1);
    }

    [Fact]
    public void GetDriverQuery_CreateObject_ShouldBeOfTypeIRequestGetDriverQueryResponse()
    {
        typeof(GetDriverQuery).Should().Implement<IRequest<GetDriverQueryResponse>>();
    }
}