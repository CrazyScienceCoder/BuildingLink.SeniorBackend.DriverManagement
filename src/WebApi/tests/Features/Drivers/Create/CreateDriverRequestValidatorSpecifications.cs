using BuildingLink.DriverManagement.WebApi.Features.Drivers.Create;
using FluentValidation.TestHelper;

namespace BuildingLink.DriverManagement.WebApi.Tests.Features.Drivers.Create;

public class CreateDriverRequestValidatorSpecifications
{
    private readonly CreateDriverRequestValidator _validator = new();
    private const string LongString = "LongStringLongStringLongStringLongStringLongStringLongString";
    private const string LongEmail = $"{LongString}{LongString}{LongString}@domain.com";

    [Theory]
    [InlineData(null, "FirstName is required")]
    [InlineData("", "FirstName is required")]
    [InlineData("ab", "FirstName must be at least 3 characters long")]
    [InlineData(LongString, "FirstName must be less than or equal to 50 characters long")]
    public void CreateDriverRequestValidator_InvalidFirstName_ShouldReturnValidationError(string? firstName, string expectedError)
    {
        var model = new CreateDriverRequest { FirstName = firstName! };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
              .WithErrorMessage(expectedError);
    }

    [Theory]
    [InlineData(null, "LastName is required")]
    [InlineData("", "LastName is required")]
    [InlineData("ab", "LastName must be at least 3 characters long")]
    [InlineData(LongString, "LastName must be less than or equal to 50 characters long")]
    public void CreateDriverRequestValidator_InvalidLastName_ShouldReturnValidationError(string? lastName, string expectedError)
    {
        var model = new CreateDriverRequest { LastName = lastName! };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.LastName)
              .WithErrorMessage(expectedError);
    }

    [Theory]
    [InlineData(null, "Email is required")]
    [InlineData("", "Email is required")]
    [InlineData("invalid-email", "Invalid email format")]
    [InlineData(LongEmail, "Email must be less than or equal to 150 characters long")]
    public void CreateDriverRequestValidator_InvalidEmail_ShouldReturnValidationError(string? email, string expectedError)
    {
        var model = new CreateDriverRequest { Email = email! };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(expectedError);
    }

    [Theory]
    [InlineData(null, "Phone number is required")]
    [InlineData("", "Phone number is required")]
    [InlineData("123", "Invalid phone number format")]
    [InlineData("abcdefghijk", "Invalid phone number format")]
    [InlineData("+1234567890123456", "Invalid phone number format")]
    public void CreateDriverRequestValidator_InvalidPhoneNumber_ShouldReturnValidationError(string? phoneNumber, string expectedError)
    {
        var model = new CreateDriverRequest { PhoneNumber = phoneNumber! };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
              .WithErrorMessage(expectedError);
    }

    [Fact]
    public void CreateDriverRequestValidator_ValidModel_ShouldNotHaveAnyValidationErrors()
    {
        var model = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "+12345678901"
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
