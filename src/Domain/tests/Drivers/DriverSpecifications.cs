using BuildingLink.DriverManagement.Domain.Types;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Domain.Tests.Drivers;

public partial class DriverSpecifications
{
    [Fact]
    public void Driver_AttemptToCreateObject_ShouldSucceed()
    {
        var driver = TestBuilder.CreateDriver();

        driver.Should().NotBeNull();
    }

    [Fact]
    public void Driver_CreateObject_ShouldHaveIdFirstNameLastNameEmailPhoneNumber()
    {
        var driver = TestBuilder.CreateDriver();

        driver.Should().NotBeNull();
        driver.Id.Should().NotBeNull();
        driver.FirstName.Should().NotBeNull();
        driver.LastName.Should().NotBeNull();
        driver.Email.Should().NotBeNull();
        driver.PhoneNumber.Should().NotBeNull();
    }

    [Fact]
    public void Driver_CreateObject_ShouldHaveIdOfTypeId()
    {
        var driver = TestBuilder.CreateDriver();

        driver.Id.Should().NotBeNull();
        driver.Id.Should().BeOfType<Id>();
    }

    [Fact]
    public void Driver_CreateObject_ShouldHaveFirstNameOfTypeName()
    {
        var driver = TestBuilder.CreateDriver();

        driver.FirstName.Should().NotBeNull();
        driver.FirstName.Should().BeOfType<Name>();
    }

    [Fact]
    public void Driver_CreateObject_ShouldHaveLastNameOfTypeName()
    {
        var driver = TestBuilder.CreateDriver();

        driver.LastName.Should().NotBeNull();
        driver.LastName.Should().BeOfType<Name>();
    }

    [Fact]
    public void Driver_CreateObject_ShouldHaveEmailOfTypeEmail()
    {
        var driver = TestBuilder.CreateDriver();

        driver.Email.Should().NotBeNull();
        driver.Email.Should().BeOfType<Email>();
    }

    [Fact]
    public void Driver_CreateObject_ShouldHavePhoneNumberOfTypePhoneNumber()
    {
        var driver = TestBuilder.CreateDriver();

        driver.PhoneNumber.Should().NotBeNull();
        driver.PhoneNumber.Should().BeOfType<PhoneNumber>();
    }
}