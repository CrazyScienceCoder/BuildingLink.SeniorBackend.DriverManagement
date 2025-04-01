using MediatR;

namespace BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;

public class GetAlphabetizedCollectionQuery : IRequest<GetAlphabetizedCollectionQueryResponse>
{
    public int? PageSize { get; set; }

    public int? PageNumber { get; set; }
}