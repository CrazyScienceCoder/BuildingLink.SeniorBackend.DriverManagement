using System.Text.RegularExpressions;
using BuildingLink.DriverManagement.Application.Drivers.Create;
using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Exceptions;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Create;

public partial class CreateDriverCommandHandlerSpecifications
{
    [Fact]
    public void CreateDriverCommandHandler_AttemptToCreateObject_ShouldSucceed()
    {
        var testBuilder = new TestBuilder();

        var handler = testBuilder.Build();

        handler.Should().NotBeNull();
    }

    [Fact]
    public void CreateDriverCommandHandler_CreateObject_ShouldBeOfTypeIRequestHandlerCreateDriverCommand()
    {
        typeof(CreateDriverCommandHandler).Should().Implement<IRequestHandler<CreateDriverCommand, CreateDriverCommandResponse>>();
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldSendDriverObjectToDriverRepository()
    {
        const int driverId = 1;

        var testBuilder = new TestBuilder()
            .SetupDriverRepository(driverId: driverId);

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultCommand, CancellationToken.None);

        results.Should().NotBeNull();
        results.Data.Should().NotBeNull();
        results.Data.Id.Should().Be(driverId);
        results.IsSuccess.Should().BeTrue();

        testBuilder.DriverRepositoryMock.Verify(s => s.CreateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DomainValidationException_ShouldValidationError()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepositoryDomainValidationException();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultCommand, CancellationToken.None);

        results.Should().NotBeNull();
        results.IsSuccess.Should().BeFalse();
        results.Data.Should().BeNull();
        results.Error.Should().NotBeNull();
        results.Error.Should().BeOfType<InvalidEmailException>();
        results.ErrorType.Should().Be(ErrorType.ValidationError);

        testBuilder.DriverRepositoryMock.Verify(s => s.CreateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DriverRepositoryThrowException_ShouldReturnsFailureResults()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepositoryException();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultCommand, CancellationToken.None);

        results.Should().NotBeNull();
        results.IsSuccess.Should().BeFalse();
        results.Data.Should().BeNull();
        results.Error.Should().NotBeNull();
        results.Error.Should().BeOfType<Exception>();
        results.ErrorType.Should().Be(ErrorType.Generic);

        testBuilder.DriverRepositoryMock.Verify(s => s.CreateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DriverRepositoryThrowException_ShouldLogTheException()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepositoryException();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultCommand, CancellationToken.None);

        results.Should().NotBeNull();

        testBuilder.LoggerMock.Verify(x =>
            x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<object>(y =>
                    y.ToString()!.Contains(
                        $"Something went wrong while executing the handler, handler type: {nameof(CreateDriverCommandHandler)}")),
                It.Is<Exception>(e => e.Message == "Test Exception"),
                ((Func<object, Exception, string>)It.IsAny<object>())!
            ), Times.Once);
    }

    [Fact]
    public async Task Handle_AfterExecution_ShouldLogTheElapsedTime()
    {
        const int driverId = 1;

        var testBuilder = new TestBuilder()
            .SetupDriverRepository(driverId: driverId);

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultCommand, CancellationToken.None);

        results.Should().NotBeNull();

        testBuilder.LoggerMock.Verify(x =>
            x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<object>((state, t) =>
                    Regex.IsMatch(
                        state.ToString()!, @$"^Handler: {nameof(CreateDriverCommandHandler)}, ElapsedTime \d+(\.\d+)? ms$")),
                It.IsAny<Exception>(),
                ((Func<object, Exception, string>)It.IsAny<object>())!
            ), Times.Once);
    }
}
