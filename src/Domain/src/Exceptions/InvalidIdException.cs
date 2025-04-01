namespace BuildingLink.DriverManagement.Domain.Exceptions;

public class InvalidIdException(string message, string parameterName) : DomainValidationException(message, parameterName);