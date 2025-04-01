namespace BuildingLink.DriverManagement.Domain.Exceptions;

public sealed class InvalidPhoneNumberException(string message, string parameterName) : DomainValidationException(message, parameterName);