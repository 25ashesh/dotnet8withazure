using Xunit;
using FrontLineCleaners.Infrastructure.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontLineCleaners.Application.User;
using FrontLineCleaners.Domain.Entities;
using Moq;
using FrontLineCleaners.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using FluentAssertions;
using FrontLineCleaners.Application.Commands;

namespace FrontLineCleaners.Infrastructure.Tests.Authorization.Requirements;

public class MultipleCleanerCompanyOwnedRequirementHandlerTests
{
    private readonly Mock<ILogger<MultipleCleanerCompanyOwnedRequirementHandler>> _loggerMock;
    private readonly Mock<ICleanersRepository> _cleanersRepositoryMock;
    public MultipleCleanerCompanyOwnedRequirementHandlerTests()
    {
        _loggerMock = new Mock<ILogger<MultipleCleanerCompanyOwnedRequirementHandler>>();
        _cleanersRepositoryMock = new Mock<ICleanersRepository>();
    }
    [Fact()]
    public async Task HandleRequirementAsync_UserHasOwnedMultipleCleanerCompanies_ShouldSucceed()
    {
        //arrange
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

        var cleaners = new List<Cleaner>
        {
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = "2",
            }
        };

        _cleanersRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(cleaners);

        var requirement = new MultipleCleanerCompanyOwnedRequirement(2);
        var handler = new MultipleCleanerCompanyOwnedRequirementHandler(_loggerMock.Object,
            userContextMock.Object, _cleanersRepositoryMock.Object);

        var context = new AuthorizationHandlerContext([requirement],null,null);

        //act
        await handler.HandleAsync(context);

        //assert
        context.HasSucceeded.Should().BeTrue();
    }

    [Fact()]
    public async Task HandleRequirementAsync_UserHasNotOwnedMultipleCleanerCompanies_ShouldFail()
    {
        //arrange
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

        var cleaners = new List<Cleaner>
        {
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = "2",
            }
        };

        _cleanersRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(cleaners);

        var requirement = new MultipleCleanerCompanyOwnedRequirement(2);
        var handler = new MultipleCleanerCompanyOwnedRequirementHandler(_loggerMock.Object,
            userContextMock.Object, _cleanersRepositoryMock.Object);

        var context = new AuthorizationHandlerContext([requirement], null, null);

        //act
        await handler.HandleAsync(context);

        //assert
        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeTrue();
    }
}