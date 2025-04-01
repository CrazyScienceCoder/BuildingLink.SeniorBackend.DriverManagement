namespace BuildingLink.DriverManagement.Application.Shared.Mappers;

public static class ExceptionMapper
{
    public static TResponse ToFailedResponse<TResponse, TData>(this Exception exception, ErrorType errorType)
        where TResponse : Result<TData, TResponse>, new()
    {
        return Result<TData, TResponse>.Failure(exception: exception, errorType: errorType, message: exception.Message);
    }
}