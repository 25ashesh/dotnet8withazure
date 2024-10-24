using Xunit;
using FrontLineCleaners.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FrontLineCleaners.Application.Dtos;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Repositories;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace FrontLineCleaners.API.Tests.Controllers;

public class CleanersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<ICleanersRepository> _cleanersRepositoryMock = new();

    public CleanersControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped(typeof(ICleanersRepository),
                                                _ => _cleanersRepositoryMock.Object));
                });
            });
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

    [Fact()]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        //arrange
        var id = 9999999;

        //instead of calling the database, the repository will return null value
        _cleanersRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Cleaner?)null);
        
        var client = _factory.CreateClient();

        //act
        var response = await client.GetAsync($"/api/cleaners/{id}");

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact()]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        //arrange
        var id = 99;

        var cleaner = new Cleaner()
        {
            Id = id,
            Name = "test",
            Description = "Test Description"
        };

        //instead of calling the database, the repository will return null value
        _cleanersRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(cleaner);

        var client = _factory.CreateClient();

        //act
        var response = await client.GetAsync($"/api/cleaners/{id}");
        var cleanerDto = await response.Content.ReadFromJsonAsync<CleanerDto>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        cleanerDto.Should().NotBeNull();
        cleanerDto.Name.Should().Be("test");
        cleanerDto.Description.Should().Be("Test Description");
    }
}
