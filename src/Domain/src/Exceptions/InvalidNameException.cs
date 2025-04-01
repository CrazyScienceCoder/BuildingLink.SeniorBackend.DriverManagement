namespace BuildingLink.DriverManagement.Domain.Exceptions;

public sealed class InvalidNameException(string message, string parameterName) : DomainValidationException(message, parameterName);