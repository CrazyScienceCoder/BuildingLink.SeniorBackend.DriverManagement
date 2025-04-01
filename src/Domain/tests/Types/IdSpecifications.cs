using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Domain.Tests.Types;

public class IdSpecifications
{
    [Fact]
    public void Id_ZeroValue_ShouldSucceed()
    {
        var id = new Id(0);

        id.Should().NotBeNull();
        id.Value.Should().Be(0);
    }

    [Fact]
    public void Id_NegativeValue_ShouldThrowInvalidIdException()
    {
        var handle = () => new Id(-1);

        handle.Should().ThrowExactly<InvalidIdException>()
            .WithMessage("Id cannot be negative.*")
            .WithParameterName("id");
    }

    [Fact]
    public void Id_ImplicitCastFromIntToId_ShouldSucceed()
    {
        const int intId = 5;

        Id id = intId;

        id.Should().NotBeNull();
        id.Value.Should().Be(intId);
        id.Should().BeOfType<Id>();
    }

    [Fact]
    public void Id_ImplicitCastFromIdToInt_ShouldSucceed()
    {
        const int intId = 5;

        int id = new Id(intId);

        id.Should().Be(intId);
        id.Should().BeOfType(typeof(int));
    }
}