using Xunit;
using FrontLineCleaners.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FrontLineCleaners.API.Tests.Controllers;

public class CleanersControllerTests:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CleanersControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact()]
    public async Task GetAll_ForValidRequest_Returns200Ok()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var result = await client.GetAsync("/api/cleaners?pageNumber=1&pageSize=10");

        //assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

    }
    [Fact()]
    public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var result = await client.GetAsync("/api/cleaners");

        //assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

    }
}
