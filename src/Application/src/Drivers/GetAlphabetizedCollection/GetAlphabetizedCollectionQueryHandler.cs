using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Application.Shared.Mappers;
using BuildingLink.DriverManagement.Domain.Drivers;
using Microsoft.Extensions.Logging;

namespace BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;

public sealed class GetAlphabetizedCollectionQueryHandler(
    IDriverRepository driverRepository,
    ILogger<GetAlphabetizedCollectionQueryHandler> logger)
    : HandlerBase<GetAlphabetizedCollectionQuery, GetAlphabetizedCollectionQueryResponse, IReadOnlyList<DriverResult>,
        Driver>(driverRepository, logger)
{
    private const int DefaultPageNumber = 1;
    private const int DefaultPageSize = 1000;

    protected override async Task<GetAlphabetizedCollectionQueryResponse> ExecuteAsync(
        GetAlphabetizedCollectionQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = GetValueOrDefault(request.PageNumber, DefaultPageNumber);
        var pageSize = GetValueOrDefault(request.PageSize, DefaultPageSize);

        var drivers = await driverRepository.GetAlphabetizedAsync(pageNumber, pageSize, cancellationToken);

        var results = drivers.ToDriverResult().ToList();

        return GetAlphabetizedCollectionQueryResponse.Success(results, "Successfully retrieved drivers");
    }

    private static int GetValueOrDefault(int? input, int defaultValue)
    {
        var inputValue = input.GetValueOrDefault();

        return inputValue <= 0 ? defaultValue : inputValue;
    }
}