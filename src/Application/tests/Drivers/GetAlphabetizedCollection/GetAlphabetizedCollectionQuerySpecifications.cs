using BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;
using FluentAssertions;
using MediatR;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.GetAlphabetizedCollection;

public class GetAlphabetizedCollectionQuerySpecifications
{
    [Fact]
    public void GetAlphabetizedCollectionQuery_AttemptToCreateObject_ShouldSucceed()
    {
        var query = new GetAlphabetizedCollectionQuery();

        query.Should().NotBeNull();
    }

    [Fact]
    public void GetAlphabetizedCollectionQuery_AttemptToCreateObjectWithPageInfo_ShouldSucceed()
    {
        var query = new GetAlphabetizedCollectionQuery
        {
            PageSize = 100,
            PageNumber = 1
        };

        query.Should().NotBeNull();
        query.PageNumber.Should().Be(1);
        query.PageSize.Should().Be(100);
    }

    [Fact]
    public void GetAlphabetizedCollectionQuery_CreateObject_ShouldBeOfTypeIRequestGetAlphabetizedCollectionQueryResponse()
    {
        typeof(GetAlphabetizedCollectionQuery).Should().Implement<IRequest<GetAlphabetizedCollectionQueryResponse>>();
    }
}