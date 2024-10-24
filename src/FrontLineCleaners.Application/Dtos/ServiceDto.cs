using FrontLineCleaners.Domain.Entities;

namespace FrontLineCleaners.Application.Dtos;

public class ServiceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Cost { get; set; }
    public int? TimeInHours { get; set; }
    public int CleanerId { get; set; }

    //public static ServiceDto FromEntity(Service service)
    //{
    //    return new ServiceDto() 
    //    { 
    //        Id = service.Id,
    //        Name = service.Name,
    //        Description = service.Description,
    //        Cost = service.Cost,
    //        TimeInHours = service.TimeInHours
    //    };
    //}
}
