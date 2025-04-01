using Ardalis.GuardClauses;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types.Validators;

namespace BuildingLink.DriverManagement.Domain.Types;

public sealed record Email
{
    public const int MaxLength = 150;

    public string Value { get; init; }

    public Email(string email)
    {
        Guard.Against.NullOrEmpty(email,
            exceptionCreator: () => new InvalidEmailException("Email cannot be null or empty.", nameof(email)));

        Guard.Against.InvalidInput(email, nameof(email), s => s.IsValidEmail(),
            exceptionCreator: () => new InvalidEmailException("Invalid email format.", nameof(email)));

        Guard.Against.StringTooLong(email, MaxLength, nameof(email),
            exceptionCreator: () => new InvalidEmailException($"Email max length is {MaxLength} chars.*", nameof(email)));

        Value = email;
    }

    public static implicit operator Email(string email)
        => new(email);

    public static implicit operator string(Email email)
        => email.Value;
}