namespace BuildingLink.DriverManagement.Domain.Exceptions;

public class InvalidEmailException(string message, string parameterName) : DomainValidationException(message, parameterName);