using FrontLineCleaners.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Dtos;

public class CleanerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasOwnSupplies { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<ServiceDto> Services { get; set; } = [];

    //public static CleanerDto? FromEntity(Cleaner? cleaner)
    //{
    //    if (cleaner == null) return null;

    //    return new CleanerDto()
    //    {
    //        Category = cleaner.Category,
    //        Description = cleaner.Description,
    //        Id = cleaner.Id,
    //        HasOwnSupplies = cleaner.HasOwnSupplies,
    //        Name = cleaner.Name,
    //        City = cleaner.Address.City,
    //        Street = cleaner.Address.Street,
    //        PostalCode = cleaner.Address.PostalCode,
    //        Services = cleaner.Services.Select(ServiceDto.FromEntity).ToList(),
    //    };
    //}
}
