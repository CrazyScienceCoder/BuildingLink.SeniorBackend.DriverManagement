namespace BuildingLink.DriverManagement.Domain.Exceptions;

public sealed class InvalidIdException(string message, string parameterName) : DomainValidationException(message, parameterName);