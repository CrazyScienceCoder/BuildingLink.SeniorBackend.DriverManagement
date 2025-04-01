namespace BuildingLink.DriverManagement.Domain.Exceptions;

public sealed class InvalidEmailException(string message, string parameterName) : DomainValidationException(message, parameterName);