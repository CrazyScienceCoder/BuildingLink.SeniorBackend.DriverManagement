using BuildingLink.DriverManagement.WebApi.Features.Drivers.GetAlphabetizedCollection;
using FluentValidation.TestHelper;

namespace BuildingLink.DriverManagement.WebApi.Tests.Features.Drivers.GetAlphabetizedCollection;

public class GetAlphabetizedCollectionRequestValidatorSpecifications
{
    private readonly GetAlphabetizedCollectionRequestValidator _validator = new();

    [Theory]
    [InlineData(0, "PageSize must be greater than 0")]
    [InlineData(-1, "PageSize must be greater than 0")]
    public void GetAlphabetizedCollectionRequestValidator_InvalidPageSize_ShouldReturnValidationError(int pageSize, string expectedError)
    {
        var model = new GetAlphabetizedCollectionRequest { PageSize = pageSize };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.PageSize)
            .WithErrorMessage(expectedError);
    }

    [Theory]
    [InlineData(0, "PageNumber must be greater than 0")]
    [InlineData(-1, "PageNumber must be greater than 0")]
    public void GetAlphabetizedCollectionRequestValidator_InvalidPageNumber_ShouldReturnValidationError(int pageNumber, string expectedError)
    {
        var model = new GetAlphabetizedCollectionRequest { PageNumber = pageNumber };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.PageNumber)
            .WithErrorMessage(expectedError);
    }

    [Fact]
    public void GetAlphabetizedCollectionRequestValidator_ValidModel_ShouldNotHaveAnyValidationErrors()
    {
        var model = new GetAlphabetizedCollectionRequest
        {
            PageSize = 10,
            PageNumber = 1
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void GetAlphabetizedCollectionRequestValidator_NullValues_ShouldNotHaveAnyValidationErrors()
    {
        var model = new GetAlphabetizedCollectionRequest
        {
            PageSize = null,
            PageNumber = null
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}