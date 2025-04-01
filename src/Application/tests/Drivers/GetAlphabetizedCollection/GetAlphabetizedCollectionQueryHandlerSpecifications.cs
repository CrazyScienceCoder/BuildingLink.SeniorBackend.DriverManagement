using System.Text.RegularExpressions;
using BuildingLink.DriverManagement.Application.Drivers.GetAlphabetizedCollection;
using BuildingLink.DriverManagement.Domain.Exceptions;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.GetAlphabetizedCollection;

public partial class GetAlphabetizedCollectionQueryHandlerSpecifications
{
    [Fact]
    public void GetAlphabetizedCollectionQueryHandler_AttemptToCreateObject_ShouldSucceed()
    {
        var testBuilder = new TestBuilder();

        var handler = testBuilder.Build();

        handler.Should().NotBeNull();
    }

    [Fact]
    public void GetAlphabetizedCollectionQueryHandler_CreateObject_ShouldBeOfTypeIRequestHandlerGetDriverQuery()
    {
        typeof(GetAlphabetizedCollectionQueryHandler).Should().Implement<IRequestHandler<GetAlphabetizedCollectionQuery, GetAlphabetizedCollectionQueryResponse>>();
    }

    [Theory]
    [InlineData(null, null, 1, 1000)]
    [InlineData(null, 100, 1, 100)]
    [InlineData(1, null, 1, 1000)]
    [InlineData(10, null, 10, 1000)]
    [InlineData(10, 50, 10, 50)]
    [InlineData(0, 0, 1, 1000)]
    public async Task Handle_ValidQuery_ShouldSendPagingInfoToDriverRepository(int? pageNumber, int? pageSize, int expectedPageNumber, int expectedPageSize)
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepository();

        var query = new GetAlphabetizedCollectionQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var handler = testBuilder.Build();

        var results = await handler.Handle(query, CancellationToken.None);

        results.Should().NotBeNull();
        results.Data.Should().NotBeNull();

        testBuilder.DriverRepositoryMock.Verify(
            s => s.GetAlphabetizedAsync(It.Is<int>(n => n == expectedPageNumber),
                It.Is<int>(n => n == expectedPageSize), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DriverRepositoryThrowException_ShouldReturnsFailureResults()
    {
        const int expectedPageNumber = 1;
        const int expectedPageSize = 1000;

        var testBuilder = new TestBuilder()
            .SetupDriverRepositoryException();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();
        results.IsSuccess.Should().BeFalse();
        results.Data.Should().BeNull();
        results.Error.Should().NotBeNull();
        results.Error.Should().BeOfType<Exception>();

        testBuilder.DriverRepositoryMock.Verify(
            s => s.GetAlphabetizedAsync(It.Is<int>(n => n == expectedPageNumber),
                It.Is<int>(n => n == expectedPageSize), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DomainValidationException_ShouldReturnsFailureResults()
    {
        const int expectedPageNumber = 1;
        const int expectedPageSize = 1000;

        var testBuilder = new TestBuilder()
            .SetupDriverRepositoryDomainValidationException();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();
        results.IsSuccess.Should().BeFalse();
        results.Data.Should().BeNull();
        results.Error.Should().NotBeNull();
        results.Error.Should().BeOfType<InvalidEmailException>();

        testBuilder.DriverRepositoryMock.Verify(
            s => s.GetAlphabetizedAsync(It.Is<int>(n => n == expectedPageNumber),
                It.Is<int>(n => n == expectedPageSize), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DriverRepositoryThrowException_ShouldLogTheException()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepositoryException();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();

        testBuilder.LoggerMock.Verify(x =>
            x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<object>(y =>
                    y.ToString()!.Contains(
                        $"Something went wrong while executing the handler, handler type: {nameof(GetAlphabetizedCollectionQueryHandler)}")),
                It.Is<Exception>(e => e.Message == "Test Exception"),
                ((Func<object, Exception, string>)It.IsAny<object>())!
            ), Times.Once);
    }

    [Fact]
    public async Task Handle_AfterExecution_ShouldLogTheElapsedTime()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepository();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();

        testBuilder.LoggerMock.Verify(x =>
            x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<object>((state, t) =>
                    Regex.IsMatch(
                        state.ToString()!, @$"^Handler: {nameof(GetAlphabetizedCollectionQueryHandler)}, ElapsedTime \d+(\.\d+)? ms$")),
                It.IsAny<Exception>(),
                ((Func<object, Exception, string>)It.IsAny<object>())!
            ), Times.Once);
    }
}