using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Domain.Tests.Types;

public class NameSpecifications
{
    [Fact]
    public void Name_NonEmptyOrNullValue_ShouldCreateName()
    {
        const string nameValue = "test";

        var name = new Name(nameValue);

        name.Should().NotBeNull();
        name.Value.Should().Be(nameValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Name_EmptyOrNullValue_ShouldThrowInvalidNameException(string? value)
    {
        var handle = () => new Name(value!);

        handle.Should().ThrowExactly<InvalidNameException>()
            .WithMessage("Name cannot be null or empty.*")
            .WithParameterName("name");
    }

    [Fact]
    public void Name_LongName_ShouldThrowInvalidNameException()
    {
        var handle = () => new Name($"{new string('a', 51)}");

        handle.Should().ThrowExactly<InvalidNameException>()
            .WithMessage("Name max length is 50 chars.*")
            .WithParameterName("name");
    }

    [Fact]
    public void Name_ImplicitCastFromStringToName_ShouldSucceed()
    {
        const string nameValue = "test";

        Name name = nameValue;

        name.Should().NotBeNull();
        name.Value.Should().Be(nameValue);
        name.Should().BeOfType<Name>();
    }

    [Fact]
    public void Name_ImplicitCastFromNameToString_ShouldSucceed()
    {
        const string nameValue = "test";

        string name = new Name(nameValue);

        name.Should().NotBeNull();
        name.Should().Be(nameValue);
        name.Should().BeOfType<string>();
    }

    [Fact]
    public void Name_ValidNameValue_ShouldReturnTheAlphabetizedName()
    {
        const string nameValue = "Oliver";
        const string alphabetizedName = "eilOrv";

        var name = new Name(nameValue);

        name.Should().NotBeNull();
        name.Value.Should().Be(nameValue);
        name.AlphabetizedValue.Should().Be(alphabetizedName);
    }
}