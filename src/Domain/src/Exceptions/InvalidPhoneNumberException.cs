namespace BuildingLink.DriverManagement.Domain.Exceptions;

public class InvalidPhoneNumberException(string message, string parameterName) : DomainValidationException(message, parameterName);