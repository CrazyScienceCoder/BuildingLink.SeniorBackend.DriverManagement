namespace BuildingLink.DriverManagement.Application.Shared;

public abstract class Result<TData, TResponse> where TResponse : Result<TData, TResponse>, new()
{
    private const string DefaultSuccessMessage = "Successfully executed";
    private const string DefaultFailureMessage = "Failed to execute";

    public bool IsSuccess { get; private init; }

    public string? Message { get; private init; }

    public TData? Data { get; private init; }

    public Exception? Error { get; private init; }

    public ErrorType? ErrorType { get; set; }

    public static TResponse Success(TData? data = default, string message = DefaultSuccessMessage)
    {
        return new TResponse
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }

    public static TResponse Failure(Exception? exception = null, ErrorType? errorType = null, string message = DefaultFailureMessage)
    {
        return new TResponse
        {
            IsSuccess = false,
            Message = message,
            Error = exception,
            ErrorType = errorType
        };
    }
}