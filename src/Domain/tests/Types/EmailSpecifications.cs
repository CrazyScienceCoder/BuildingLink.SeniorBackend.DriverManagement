using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Domain.Tests.Types;

public class EmailSpecifications
{
    [Fact]
    public void Email_NonEmptyOrNullValue_ShouldCreateEmail()
    {
        const string emailValue = "test@domain.com";

        var name = new Email(emailValue);

        name.Should().NotBeNull();
        name.Value.Should().Be(emailValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Email_EmptyOrNullValue_ShouldThrowInvalidEmailException(string? value)
    {
        var handle = () => new Email(value!);

        handle.Should().ThrowExactly<InvalidEmailException>()
            .WithMessage("Email cannot be null or empty.*")
            .WithParameterName("email");
    }

    [Fact]
    public void Email_LongEmail_ShouldThrowInvalidEmailException()
    {
        var handle = () => new Email($"{new string('a', 200)}@domain.com");

        handle.Should().ThrowExactly<InvalidEmailException>()
            .WithMessage("Email max length is 150 chars.*")
            .WithParameterName("email");
    }

    [Theory]
    [InlineData("a wrong email")]
    [InlineData("123108.com")]
    public void Email_InvalidEmailFormat_ShouldThrowInvalidEmailException(string? value)
    {
        var handle = () => new Email(value!);

        handle.Should().ThrowExactly<InvalidEmailException>()
            .WithMessage("Invalid email format.*")
            .WithParameterName("email");
    }

    [Fact]
    public void Email_ImplicitCastFromStringToEmail_ShouldSucceed()
    {
        const string emailValue = "test@domain.com";

        Email email = emailValue;

        email.Should().NotBeNull();
        email.Value.Should().Be(emailValue);
        email.Should().BeOfType<Email>();
    }

    [Fact]
    public void Email_ImplicitCastFromEmailToString_ShouldSucceed()
    {
        const string emailValue = "test@domain.com";

        string email = new Email(emailValue);

        email.Should().NotBeNull();
        email.Should().Be(emailValue);
        email.Should().BeOfType<string>();
    }
}
