using Xunit;
using FrontLineCleaners.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FrontLineCleaners.Application.Commands;
using FrontLineCleaners.Domain.Entities;

namespace FrontLineCleaners.Application.Tests.Dtos;

public class CleanersProfileTests
{
    private readonly IMapper _mapper;
    public CleanersProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CleanersProfile>();
        });
        _mapper = configuration.CreateMapper();
    }
    [Fact()]
    public void CreateMap_ForCleanerToCleanerDto_MapsCorrectly()
    {
        //arrange
        var cleaner1 = new Cleaner()
        {
            Id = 1,
            Name = "Test Cleaners",
            Description = "Test Cleaner Description",
            Category = "Test Category",
            HasOwnSupplies = true,
            ContactEmail = "test@flc.dev.uk",
            ContactNumber = "1234567098",
            Address = new Address()
            {
                City = "Test city1",
                Street = "Test Street",
                PostalCode = "123456"
            }
        };

        //act
        var cleanerDto = _mapper.Map<CleanerDto>(cleaner1);

        //assert
        cleanerDto.Should().NotBeNull();
        cleanerDto.Id.Should().Be(cleaner1.Id);
        cleanerDto.Name.Should().Be(cleaner1.Name);

        cleanerDto.Description.Should().Be(cleaner1.Description);
        cleanerDto.Category.Should().Be(cleaner1.Category);
        cleanerDto.HasOwnSupplies.Should().Be(cleaner1.HasOwnSupplies);

        cleanerDto.City.Should().Be(cleaner1.Address.City);
        cleanerDto.Street.Should().Be(cleaner1.Address.Street);
        cleanerDto.PostalCode.Should().Be(cleaner1.Address.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForCleanerCommandToCleaner_MapsCorrectly()
    {
        //arrange
        var command = new CreateCleanerCommand()
        {
            Name = "Test Cleaners",
            Description = "Test Cleaner Description",
            Category = "Test Category",
            HasOwnSupplies = true,
            ContactEmail = "test@flc.dev.uk",
            ContactNumber = "1234567098",
            City = "Test city1",
            Street = "Test Street",
            PostalCode = "123456"
        };


        //act
        var cleaner = _mapper.Map<Cleaner>(command);

        //assert
        cleaner.Should().NotBeNull();
        cleaner.Name.Should().Be(command.Name);

        cleaner.Description.Should().Be(command.Description);
        cleaner.Category.Should().Be(command.Category);
        cleaner.HasOwnSupplies.Should().Be(command.HasOwnSupplies);

        cleaner.Address.City.Should().Be(command.City);
        cleaner.Address.Street.Should().Be(command.Street);
        cleaner.Address.PostalCode.Should().Be(command.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForUpdateCommandToCleaner_MapsCorrectly()
    {
        //arrange
        var command = new UpdateCleanerCommand()
        {
            Id = 1,
            Name = "Test Cleaners",
            Description = "Test Cleaner Description",
            HasOwnSupplies = true,
        };


        //act
        var cleaner = _mapper.Map<Cleaner>(command);

        //assert
        cleaner.Should().NotBeNull();
        cleaner.Id.Should().Be(command.Id);
        cleaner.Name.Should().Be(command.Name);
        cleaner.Description.Should().Be(command.Description);
        cleaner.HasOwnSupplies.Should().Be(command.HasOwnSupplies);
    }
}
