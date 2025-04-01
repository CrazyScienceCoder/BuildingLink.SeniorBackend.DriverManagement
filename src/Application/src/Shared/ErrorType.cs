using Ardalis.SmartEnum;

namespace BuildingLink.DriverManagement.Application.Shared;

public sealed class ErrorType : SmartEnum<ErrorType>
{
    public static readonly ErrorType Generic = new(nameof(Generic), 1);
    public static readonly ErrorType RecordNotFound = new(nameof(RecordNotFound), 2);
    public static readonly ErrorType ValidationError = new(nameof(ValidationError), 3);

    private ErrorType(string name, int value) : base(name, value)
    {
    }
}
