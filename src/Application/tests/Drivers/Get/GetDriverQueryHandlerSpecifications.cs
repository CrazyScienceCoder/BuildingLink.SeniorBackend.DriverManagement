using System.Text.RegularExpressions;
using BuildingLink.DriverManagement.Application.Drivers;
using BuildingLink.DriverManagement.Application.Drivers.Get;
using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Domain.Exceptions;
using BuildingLink.DriverManagement.Domain.Types;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Get;

public partial class GetDriverQueryHandlerSpecifications
{
    [Fact]
    public void GetDriverQueryHandler_AttemptToCreateObject_ShouldSucceed()
    {
        var testBuilder = new TestBuilder();

        var handler = testBuilder.Build();

        handler.Should().NotBeNull();
    }

    [Fact]
    public void GetDriverQueryHandler_CreateObject_ShouldBeOfTypeIRequestHandlerGetDriverQuery()
    {
        typeof(GetDriverQueryHandler).Should().Implement<IRequestHandler<GetDriverQuery, GetDriverQueryResponse>>();
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldSendDriverIdToDriverRepository()
    {
        const int driverId = 1;

        var testBuilder = new TestBuilder()
            .SetupDriverRepository();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();
        results.Data.Should().NotBeNull();

        testBuilder.DriverRepositoryMock.Verify(s => s.GetAsync(It.Is<Id>(id => id == driverId), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnDriverInfo()
    {
        const int driverId = 1;

        var expectedDriver = new DriverResult
        {
            Id = 1,
            Email = "test@domain.com",
            FirstName = "Oliver",
            LastName = "Johnson",
            PhoneNumber = "+1-404-724-1937",
            FullName = "Oliver Johnson",
            AlphabetizedFullName = "eilOrv hJnnoos"
        };

        var testBuilder = new TestBuilder()
            .SetupDriverRepository();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();
        results.Data.Should().BeEquivalentTo(expectedDriver);

        testBuilder.DriverRepositoryMock.Verify(s => s.GetAsync(It.Is<Id>(id => id == driverId), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DriverRepositoryThrowException_ShouldReturnsFailureResults()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepositoryException();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();
        results.IsSuccess.Should().BeFalse();
        results.Data.Should().BeNull();
        results.Error.Should().NotBeNull();
        results.Error.Should().BeOfType<Exception>();
        results.ErrorType.Should().Be(ErrorType.Generic);

        testBuilder.DriverRepositoryMock.Verify(s => s.GetAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DomainValidationException_ShouldReturnsFailureResults()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepositoryDomainValidationException();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();
        results.IsSuccess.Should().BeFalse();
        results.Data.Should().BeNull();
        results.Error.Should().NotBeNull();
        results.Error.Should().BeOfType<InvalidEmailException>();
        results.ErrorType.Should().Be(ErrorType.ValidationError);

        testBuilder.DriverRepositoryMock.Verify(s => s.GetAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_RecordDoesNotExist_ShouldReturnsFailureResults()
    {
        var testBuilder = new TestBuilder()
            .SetupEmptyDriverRepository();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultDriverQuery, CancellationToken.None);

        results.Should().NotBeNull();
        results.IsSuccess.Should().BeFalse();
        results.ErrorType.Should().BeOfType<ErrorType>();
        results.ErrorType.Should().Be(ErrorType.RecordNotFound);

        testBuilder.DriverRepositoryMock.Verify(s => s.GetAsync(It.IsAny<Id>(), It.IsAny<CancellationToken>()),
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
                        $"Something went wrong while executing the handler, handler type: {nameof(GetDriverQueryHandler)}")),
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
                        state.ToString()!, @$"^Handler: {nameof(GetDriverQueryHandler)}, ElapsedTime \d+(\.\d+)? ms$")),
                It.IsAny<Exception>(),
                ((Func<object, Exception, string>)It.IsAny<object>())!
            ), Times.Once);
    }
}
