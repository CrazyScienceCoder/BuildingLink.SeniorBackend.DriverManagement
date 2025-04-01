using System.Diagnostics;
using BuildingLink.DriverManagement.Application.Shared.Mappers;
using BuildingLink.DriverManagement.Domain;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingLink.DriverManagement.Application.Shared;

public abstract class HandlerBase<TCommand, TResponse, TData, TEntity>(IRepository<TEntity> repository, ILogger logger)
    : IRequestHandler<TCommand, TResponse> where TResponse : Result<TData, TResponse>, new()
    where TCommand : IRequest<TResponse>
{
    protected abstract Task<TResponse> ExecuteAsync(TCommand request, CancellationToken cancellationToken);

    public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await ExecuteAsync(request, cancellationToken);
        }
        catch (DomainValidationException domainValidationException)
        {
            logger.LogError(domainValidationException, "Invalid inputs, handler type: {HandlerType}",
                GetType().Name);

            return domainValidationException.ToFailedResponse<TResponse, TData>(ErrorType.ValidationError);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Something went wrong while executing the handler, handler type: {HandlerType}",
                GetType().Name);

            return exception.ToFailedResponse<TResponse, TData>(ErrorType.Generic);
        }
        finally
        {
            stopwatch.Stop();
            logger.LogInformation("Handler: {HandlerType}, ElapsedTime {ElapsedTime} ms", GetType().Name,
                stopwatch.Elapsed.TotalMilliseconds);
        }
    }

    protected async Task<TResponse> BuildFailureResponseAsync(Id id, CancellationToken cancellationToken)
    {
        var exists = await repository.ExistsAsync(id, cancellationToken);

        if (!exists)
        {
            return Result<TData, TResponse>.Failure(errorType: ErrorType.RecordNotFound,
                message: $"Record Not Found, Id: {id.Value}");
        }

        return Result<TData, TResponse>.Failure(errorType: ErrorType.Generic,
            message: $"Something went wrong while executing the command: {typeof(TCommand)}, Record Id: {id.Value}");
    }
}