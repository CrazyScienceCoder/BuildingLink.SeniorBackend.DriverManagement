using Ardalis.GuardClauses;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Extensions;

namespace BuildingLink.DriverManagement.Domain.Types;

public sealed record Name
{
    public const int MaxLength = 50;

    public string Value { get; init; }

    public string AlphabetizedValue => BuildAlphabetizedValue();

    private string? _cachedAlphabetizedValue;

    public Name(string name)
    {
        Guard.Against.NullOrEmpty(name,
            exceptionCreator: () => new InvalidNameException("Name cannot be null or empty.", nameof(name)));

        Guard.Against.StringTooLong(name, MaxLength, nameof(name),
            exceptionCreator: () => new InvalidNameException($"Name max length is {MaxLength} chars.*", nameof(name)));

        Value = name;
    }

    public static implicit operator Name(string name)
        => new(name);

    public static implicit operator string(Name name)
        => name.Value;

    private string BuildAlphabetizedValue()
    {
        if (_cachedAlphabetizedValue.IsNotNullOrWhiteSpace())
        {
            return _cachedAlphabetizedValue;
        }

        _cachedAlphabetizedValue = Value.ToAlphabetized();

        return _cachedAlphabetizedValue;
    }
}