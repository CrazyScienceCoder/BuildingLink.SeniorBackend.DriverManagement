using BuildingLink.DriverManagement.Domain.Types;
using FluentValidation;

namespace BuildingLink.DriverManagement.WebApi.Features.Drivers.Update;

public sealed class UpdateDriverRequestValidator : AbstractValidator<UpdateDriverRequest>
{
    public UpdateDriverRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required")
            .MinimumLength(3)
            .WithMessage("FirstName must be at least 3 characters long")
            .MaximumLength(Name.MaxLength)
            .WithMessage($"FirstName must be less than or equal to {Name.MaxLength} characters long");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("LastName is required")
            .MinimumLength(3)
            .WithMessage("LastName must be at least 3 characters long")
            .MaximumLength(Name.MaxLength)
            .WithMessage($"LastName must be less than or equal to {Name.MaxLength} characters long");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format")
            .MaximumLength(Email.MaxLength)
            .WithMessage($"Email must be less than or equal to {Email.MaxLength} characters long");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^\+?\d{10,15}$")
            .WithMessage("Invalid phone number format");
    }
}