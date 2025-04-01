using Ardalis.GuardClauses;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types.Validators;

namespace BuildingLink.DriverManagement.Domain.Types;

public record PhoneNumber
{
    public string Value { get; init; }

    public PhoneNumber(string phoneNumber)
    {
        Guard.Against.NullOrEmpty(phoneNumber,
            exceptionCreator: () => new InvalidPhoneNumberException("Phone Number cannot be null or empty.", nameof(phoneNumber)));

        Guard.Against.InvalidInput(phoneNumber, nameof(phoneNumber), s => s.IsValidPhoneNumber(),
            exceptionCreator: () => new InvalidPhoneNumberException("Invalid Phone Number format.", nameof(phoneNumber)));

        Value = phoneNumber;
    }

    public static implicit operator PhoneNumber(string phoneNumber)
        => new(phoneNumber);

    public static implicit operator string(PhoneNumber phoneNumber)
        => phoneNumber.Value;
}