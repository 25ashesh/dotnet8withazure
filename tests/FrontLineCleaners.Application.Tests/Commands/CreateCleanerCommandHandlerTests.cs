using Xunit;
using FrontLineCleaners.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FrontLineCleaners.Application.User;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace FrontLineCleaners.Application.Tests.Commands;

public class CreateCleanerCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedCleanerId()
    {
        //arrange
        var loggerMock = new Mock<ILogger<CreateCleanerCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new CreateCleanerCommand();
        var cleaner = new Cleaner();
        mapperMock.Setup(m => m.Map<Cleaner>(command)).Returns(cleaner);

        var cleanerRepositoryMock = new Mock<ICleanersRepository>();
        cleanerRepositoryMock.Setup(repo => repo.Create(It.IsAny<Cleaner>()))
            .ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var commandHandler = new CreateCleanerCommandHandler(
            loggerMock.Object,
            mapperMock.Object,
            cleanerRepositoryMock.Object, 
            userContextMock.Object);

        //act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //assert
        result.Should().Be(1);
        cleaner.OwnerId.Should().Be("owner-id");
        cleanerRepositoryMock.Verify(r => r.Create(cleaner), Times.Once);
    }
}
