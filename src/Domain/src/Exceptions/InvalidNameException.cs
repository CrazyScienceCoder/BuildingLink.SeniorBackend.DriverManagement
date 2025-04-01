namespace BuildingLink.DriverManagement.Domain.Exceptions;

public class InvalidNameException(string message, string parameterName) : DomainValidationException(message, parameterName);