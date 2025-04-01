using System.Text.RegularExpressions;
using BuildingLink.DriverManagement.Application.Drivers.Update;
using BuildingLink.DriverManagement.Application.Shared;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Exceptions;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BuildingLink.DriverManagement.Application.Tests.Drivers.Update;

public partial class UpdateDriverCommandHandlerSpecifications
{
    [Fact]
    public void UpdateDriverCommandHandler_AttemptToCreateObject_ShouldSucceed()
    {
        var testBuilder = new TestBuilder();

        var handler = testBuilder.Build();

        handler.Should().NotBeNull();
    }

    [Fact]
    public void UpdateDriverCommandHandler_CreateObject_ShouldBeOfTypeIRequestHandlerUpdateDriverCommand()
    {
        typeof(UpdateDriverCommandHandler).Should().Implement<IRequestHandler<UpdateDriverCommand, UpdateDriverCommandResponse>>();
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldSendDriverObjectToDriverRepository()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepository();

        var handler = testBuilder.Build();

        var results = await handler.Handle(TestBuilder.DefaultCommand, CancellationToken.None);

        results.Should().NotBeNull();
        results.Data.Should().NotBeNull();
        results.IsSuccess.Should().BeTrue();

        testBuilder.DriverRepositoryMock.Verify(
            s => s.UpdateAsync(It.Is<Driver>(d => d.Id > 0), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingRecord_ShouldReturnNotFoundError()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepository(isDeleteSucceeded: false, exists: false);

        var handler = testBuilder.Build();

        var command = TestBuilder.DefaultCommand;

        var results = await handler.Handle(command, CancellationToken.None);

        results.Should().NotBeNull();
        results.Data.Should().BeNull();
        results.IsSuccess.Should().BeFalse();
        results.ErrorType.Should().Be(ErrorType.RecordNotFound);

        testBuilder.DriverRepositoryMock.Verify(
            s => s.UpdateAsync(It.Is<Driver>(d => d.Id > 0), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_FailedToUpdateExistingDriver_ShouldGenericError()
    {
        var testBuilder = new TestBuilder()
            .SetupDriverRepository(isDeleteSucceeded: false, exists: true);

        var handler = testBuilder.Build();

        var command = TestBuilder.DefaultCommand;

        var results = await handler.Handle(command, CancellationToken.None);

        results.Should().NotBeNull();
        results.Data.Should().BeNull();
        results.IsSuccess.Should().BeFalse();
        results.ErrorType.Should().Be(ErrorType.Generic);

        testBuilder.DriverRepositoryMock.Verify(
            s => s.UpdateAsync(It.Is<Driver>(d => d.Id > 0), It.IsAny<CancellationToken>()),
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

        testBuilder.DriverRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DomainValidationException_ShouldReturnsFailureResults()
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

        testBuilder.DriverRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<Driver>(), It.IsAny<CancellationToken>()),
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
                        $"Something went wrong while executing the handler, handler type: {nameof(UpdateDriverCommandHandler)}")),
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

        var results = await handler.Handle(TestBuilder.DefaultCommand, CancellationToken.None);

        results.Should().NotBeNull();

        testBuilder.LoggerMock.Verify(x =>
            x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<object>((state, t) =>
                    Regex.IsMatch(
                        state.ToString()!, @$"^Handler: {nameof(UpdateDriverCommandHandler)}, ElapsedTime \d+(\.\d+)? ms$")),
                It.IsAny<Exception>(),
                ((Func<object, Exception, string>)It.IsAny<object>())!
            ), Times.Once);
    }
}