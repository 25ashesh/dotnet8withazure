using Xunit;
using FrontLineCleaners.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FrontLineCleaners.Domain.Constants;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Exceptions;
using FrontLineCleaners.Domain.Interfaces;
using FrontLineCleaners.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace FrontLineCleaners.Application.Tests.Commands;
public class UpdateCleanerCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateCleanerCommandHandler>> _loggerMock;
    private readonly Mock<ICleanersRepository> _cleanersRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICleanerAuthorizationService> _cleanerAuthorizationServiceMock;

    private readonly UpdateCleanerCommandHandler _handler;

    public UpdateCleanerCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateCleanerCommandHandler>>();
        _cleanersRepositoryMock = new Mock<ICleanersRepository>();
        _mapperMock = new Mock<IMapper>();
        _cleanerAuthorizationServiceMock = new Mock<ICleanerAuthorizationService>();

        _handler = new UpdateCleanerCommandHandler(_loggerMock.Object,
            _mapperMock.Object,
            _cleanersRepositoryMock.Object,
            _cleanerAuthorizationServiceMock.Object);
    }
    [Fact()]
    public async Task Handle_WithValidRequest_ShouldUpdateCleaners()
    {
        //arrange
        var cleanerId = 1;
        var command = new UpdateCleanerCommand()
        {
            Id = cleanerId,
            Name = "New Test",
            Description = "New Description",
            HasOwnSupplies = true
        };

        var cleaner = new Cleaner()
        {
            Id = cleanerId,
            Name = "Test",
            Description = "Test",
        };

        _cleanersRepositoryMock.Setup(r => r.GetByIdAsync(cleanerId))
            .ReturnsAsync(cleaner);

        _cleanerAuthorizationServiceMock.Setup(m => m.Authorize(cleaner, Domain.Constants.ResourceOperation.Update))
            .Returns(true);

        //act
        await _handler.Handle(command, CancellationToken.None);

        //assert
        _cleanersRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, cleaner), Times.Once);
    }

    [Fact()]
    public async Task Handle_WithNonExistingCleaner_ShouldThrowNotFoundException()
    {
        //arrange
        var cleanerId = 2;
        var request = new UpdateCleanerCommand()
        {
            Id = cleanerId
        };
        _cleanersRepositoryMock.Setup(c => c.GetByIdAsync(cleanerId))
            .ReturnsAsync((Cleaner?)null);

        //act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Cleaner with id: {cleanerId} doesn't exist!");
    }

    [Fact()]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        //arrange
        var cleanerId = 3;
        var request = new UpdateCleanerCommand()
        {
            Id = cleanerId
        };
        var existingCleaner = new Cleaner()
        {
            Id = cleanerId
        };
        _cleanersRepositoryMock
            .Setup(c => c.GetByIdAsync(cleanerId))
                .ReturnsAsync(existingCleaner);

        _cleanerAuthorizationServiceMock
            .Setup(a => a.Authorize(existingCleaner, ResourceOperation.Update))
            .Returns(false);

        //act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<ForbidException>();
    }
}