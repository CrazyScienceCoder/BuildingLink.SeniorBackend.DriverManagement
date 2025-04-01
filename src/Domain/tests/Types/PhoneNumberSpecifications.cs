using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Domain.Tests.Types;

public class PhoneNumberSpecifications
{
    [Theory]
    [InlineData("+12025550101")]
    [InlineData("+1-404-724-1937")]
    public void PhoneNumber_NonEmptyOrNullValue_ShouldCreatePhoneNumber(string phoneNumberValue)
    {
        var name = new PhoneNumber(phoneNumberValue);

        name.Should().NotBeNull();
        name.Value.Should().Be(phoneNumberValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void PhoneNumber_EmptyOrNullValue_ShouldThrowInvalidPhoneNumberException(string? value)
    {
        var handle = () => new PhoneNumber(value!);

        handle.Should().ThrowExactly<InvalidPhoneNumberException>()
            .WithMessage("Phone Number cannot be null or empty.*")
            .WithParameterName("phoneNumber");
    }

    [Theory]
    [InlineData("a wrong phone number")]
    [InlineData("123a*")]
    [InlineData("442211")]
    public void PhoneNumber_InvalidEmailFormat_ShouldThrowInvalidPhoneNumberException(string? value)
    {
        var handle = () => new PhoneNumber(value!);

        handle.Should().ThrowExactly<InvalidPhoneNumberException>()
            .WithMessage("Invalid Phone Number format.*")
            .WithParameterName("phoneNumber");
    }

    [Fact]
    public void PhoneNumber_ImplicitCastFromStringToPhoneNumber_ShouldSucceed()
    {
        const string phoneNumberValue = "+1-404-724-1937";

        PhoneNumber phoneNumber = phoneNumberValue;

        phoneNumber.Should().NotBeNull();
        phoneNumber.Value.Should().Be(phoneNumberValue);
        phoneNumber.Should().BeOfType<PhoneNumber>();
    }

    [Fact]
    public void PhoneNumber_ImplicitCastFromPhoneNumberToString_ShouldSucceed()
    {
        const string phoneNumberValue = "+1-404-724-1937";

        string phoneNumber = new PhoneNumber(phoneNumberValue);

        phoneNumber.Should().NotBeNull();
        phoneNumber.Should().Be(phoneNumberValue);
        phoneNumber.Should().BeOfType<string>();
    }
}