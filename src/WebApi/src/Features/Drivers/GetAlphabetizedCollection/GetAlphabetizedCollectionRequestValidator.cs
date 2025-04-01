using FluentValidation;

namespace BuildingLink.DriverManagement.WebApi.Features.Drivers.GetAlphabetizedCollection;

public class GetAlphabetizedCollectionRequestValidator : AbstractValidator<GetAlphabetizedCollectionRequest>
{
    public GetAlphabetizedCollectionRequestValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0");
    }
}
